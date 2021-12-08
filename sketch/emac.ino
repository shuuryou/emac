#pragma GCC diagnostic error "-Wall"
#pragma GCC diagnostic error "-Wextra"

#include "emac.h"

unsigned long     FLED_LAST_UPDATE = 0;

SoftwareWire      IVAD_I2C_BUS(PIN_IVAD_SDA, PIN_IVAD_SCL);

ezButton          POWER_BUTTON(PIN_PWRBTN);
unsigned long     POWER_BUTTON_PUSH_TIME;
byte              POWER_BUTTON_STATE;

byte              SERIAL_BUFFER[SERIAL_BUFFER_MAX_SIZE];
byte              SERIAL_BUFFER_DATA_SIZE;
boolean           SERIAL_HAVE_DATA;

byte              POWER_STATE;

void setup()
{
  pinMode(PIN_RELAY_CRT,     OUTPUT);
  digitalWrite(PIN_RELAY_CRT, RELAY_OFF);
  pinMode(PIN_RELAY_AMP,     OUTPUT);
  digitalWrite(PIN_RELAY_AMP, RELAY_OFF);
  pinMode(PIN_RELAY_BTN,     OUTPUT);
  digitalWrite(PIN_RELAY_BTN, RELAY_OFF);

  POWER_BUTTON_PUSH_TIME = 0;
  POWER_BUTTON_STATE = PWRBTN_NONE;
  POWER_STATE = PWRSTATE_DONTKNOW;
  SERIAL_HAVE_DATA = false;

  pinMode(PIN_VGA_SDA,       OUTPUT);
  pinMode(PIN_VGA_SCL,       OUTPUT);
  pinMode(PIN_IVAD_SDA,      OUTPUT);
  pinMode(PIN_IVAD_SCL,      OUTPUT);

  pinMode(PIN_PWRBTN,        INPUT_PULLUP);

  pinMode(PIN_FLED,          OUTPUT);
  pinMode(LED_BUILTIN,       OUTPUT);

  pinMode(PIN_PANDA_S3,      INPUT);
  pinMode(PIN_PANDA_S4,      INPUT);
  pinMode(PIN_PANDA_S5,      INPUT);

  digitalWrite(PIN_IVAD_SDA,  LOW);
  digitalWrite(PIN_IVAD_SCL,  LOW);
  digitalWrite(PIN_VGA_SDA,   LOW);
  digitalWrite(PIN_VGA_SCL,   LOW);

  digitalWrite(PIN_FLED,      LED_OFF);

  Serial.begin(9600);

  Wire.begin(0x50);
  Wire.onRequest(edid_on_request);

  IVAD_I2C_BUS.begin();

  EEPROMwl.begin(EEPROM_VERSION, EEPROM_SLOTS);
}

void loop()
{
  power_state_processing();
  led_processing();
  power_button_processing();
  relay_processing();
  serial_processing();
}

void power_state_processing()
{
  if (digitalRead(PIN_PANDA_S5) == HIGH && digitalRead(PIN_PANDA_S3) == HIGH && digitalRead(PIN_PANDA_S4) == HIGH)
  {
    POWER_STATE = PWRSTATE_POWERING;
    return;
  }

  if (digitalRead(PIN_PANDA_S3) == HIGH || digitalRead(PIN_PANDA_S4) == HIGH)
  {
    POWER_STATE = PWRSTATE_SLEEPING;
    return;
  }

  POWER_STATE = PWRSTATE_OFF;
}

void power_button_processing()
{
  POWER_BUTTON.loop();

  if (POWER_BUTTON_STATE == PWRBTN_IGNORE)
    return;

  if (POWER_BUTTON_STATE == PWRBTN_NONE && POWER_BUTTON.isPressed())
  {
    POWER_BUTTON_PUSH_TIME = millis();
    return;
  }

  if (POWER_BUTTON_PUSH_TIME == 0)
    return; // Button wasn't pushed at all yet

  if (POWER_BUTTON_STATE != PWRBTN_NONE && POWER_BUTTON.isReleased())
  {
    POWER_BUTTON_STATE = PWRBTN_NONE;
    POWER_BUTTON_PUSH_TIME = 0;
    return;
  }

  if (POWER_BUTTON_STATE == PWRBTN_WAITRELEASE)
    return; // Button is waiting for release

  if ((POWER_STATE == PWRSTATE_POWERING || POWER_STATE == PWRSTATE_SLEEPING) &&
      (millis() - POWER_BUTTON_PUSH_TIME) >= POWER_BUTTON_PUSH_DURATION_OFF)
  {
    POWER_BUTTON_STATE = PWRBTN_IGNORE;

    digitalWrite(PIN_RELAY_BTN, RELAY_ON);
    delay(POWER_BUTTON_RELAY_PUSH_ON_DELAY);
    digitalWrite(PIN_RELAY_BTN, RELAY_OFF);

    POWER_BUTTON_STATE = PWRBTN_WAITRELEASE;
    return;
  }

  if (POWER_STATE == PWRSTATE_SLEEPING &&
      (millis() - POWER_BUTTON_PUSH_TIME) >= POWER_BUTTON_PUSH_DURATION_ONWAKE)
  {
    POWER_BUTTON_STATE = PWRBTN_IGNORE;

    digitalWrite(PIN_RELAY_CRT, RELAY_ON);
    delay(IVAD_SETTLE_TIME);
    ivad_initialize();
    digitalWrite(PIN_RELAY_BTN, RELAY_ON);
    delay(POWER_BUTTON_RELAY_PUSH_WAKE_DELAY);
    digitalWrite(PIN_RELAY_BTN, RELAY_OFF);

    POWER_BUTTON_STATE = PWRBTN_WAITRELEASE;
    return;
  }

  if (POWER_STATE == PWRSTATE_OFF && (millis() - POWER_BUTTON_PUSH_TIME) >= POWER_BUTTON_PUSH_DURATION_ONWAKE)
  {
    POWER_BUTTON_STATE = PWRBTN_IGNORE;

    digitalWrite(PIN_RELAY_CRT, RELAY_ON);
    delay(IVAD_SETTLE_TIME);
    ivad_initialize();

    digitalWrite(PIN_RELAY_BTN, RELAY_ON);
    delay(POWER_BUTTON_RELAY_PUSH_ON_DELAY);
    digitalWrite(PIN_RELAY_BTN, RELAY_OFF);

    POWER_BUTTON_STATE = PWRBTN_WAITRELEASE;
    return;
  }
}

void relay_processing()
{
  if (POWER_STATE == PWRSTATE_POWERING)
  {
    if (digitalRead(PIN_RELAY_CRT) == RELAY_OFF)
      digitalWrite(PIN_RELAY_CRT, RELAY_ON);

    if (digitalRead(PIN_RELAY_AMP) == RELAY_OFF)
      digitalWrite(PIN_RELAY_AMP, RELAY_ON);

    return;
  }

  if (digitalRead(PIN_RELAY_AMP) == RELAY_ON)
    digitalWrite(PIN_RELAY_AMP, RELAY_OFF);

  if (digitalRead(PIN_RELAY_CRT) == RELAY_ON)
    digitalWrite(PIN_RELAY_CRT, RELAY_OFF);
}

void led_processing()
{
  if (POWER_STATE == PWRSTATE_POWERING)
  {
    if (FLED_LAST_UPDATE != 0)
      FLED_LAST_UPDATE = 0;

    if (digitalRead(PIN_FLED) != LED_ON)
      digitalWrite(PIN_FLED, LED_ON);

    return;
  }

  if (POWER_STATE == PWRSTATE_SLEEPING)
  {
    if (FLED_LAST_UPDATE == 0)
    {
      digitalWrite(PIN_FLED, LED_OFF);
      FLED_LAST_UPDATE = millis();
      return;
    }

    if ((digitalRead(PIN_PANDA_S3) == HIGH) && (millis() - FLED_LAST_UPDATE < FLED_PANDA_S3_FLASHING_RATE) ||
        (digitalRead(PIN_PANDA_S4) == HIGH) && (millis() - FLED_LAST_UPDATE < FLED_PANDA_S4_FLASHING_RATE))
    {
      return;
    }

    digitalWrite(PIN_FLED, !digitalRead(PIN_FLED));
    FLED_LAST_UPDATE = millis();
    return;
  }

  // From here POWER_STATE is PWRSTATE_OFF or PWRSTATE_DONTKNOW
  if (FLED_LAST_UPDATE != 0)
    FLED_LAST_UPDATE = 0;

  if (digitalRead(PIN_FLED) != LED_OFF)
    digitalWrite(PIN_FLED, LED_OFF);
}


void serial_processing()
{
  /* Unlike the iMac G3 protocol, the eMac protocol is more simple.

     You can talk to IVAD directly:
     ------------------------------
     You send: { SERIAL_START_IVAD, Target, Register, Value, SERIAL_END_MARKER }
     I answer: { SERIAL_START_MARKER, SERIAL_OK_MARKER or SERIAL_ERROR_MARKER, SERIAL_END_MARKER }

     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
     @ WARNING:                                                        @
     @ You can probably break your eMac CRT by writing bad values to   @
     @ the control registers. Best case only screen goes blank, worst  @
     @ case screen STAYS blank forever (i.e. CRT becomes destroyed).   @
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

     You can read EEPROM:
     --------------------
     You send: { SERIAL_START_EEPROM, SERIAL_END_MARKER }
     I answer: { SERIAL_START_MARKER, Length of EEPROM, Content of EEPROM, SERIAL_END_MARKER }

     You can verify checksum:
     ------------------------
     You send: { SERIAL_START_EEPROM, SERIAL_EEPROM_VERIFY, SERIAL_END_MARKER }
     I answer: { SERIAL_START_MARKER, SERIAL_OK_MARKER or SERIAL_ERROR_MARKER, SERIAL_END_MARKER }

     You can reset EEPROM:
     ---------------------
     You send: { SERIAL_START_EEPROM, SERIAL_EEPROM_RESET, SERIAL_END_MARKER }
     I answer: { SERIAL_START_MARKER, SERIAL_OK_MARKER, SERIAL_END_MARKER }

     You can write EEPROM:
     ---------------------
     You Send: { SERIAL_START_EEPROM, Length of EEPROM, Content of EEPROM, SERIAL_END_MARKER }
     I answer: { SERIAL_START_MARKER, SERIAL_OK_MARKER or SERIAL_ERROR_MARKER, SERIAL_END_MARKER }
  */

  /* Basic Parser */

  for (;;)
  {
    if (Serial.available() <= 0)
      break;

    if ((SERIAL_BUFFER_DATA_SIZE + 1) >= SERIAL_BUFFER_MAX_SIZE)
      SERIAL_BUFFER_DATA_SIZE = 0;

    byte b = Serial.read();
    SERIAL_BUFFER[SERIAL_BUFFER_DATA_SIZE++] = b;

    if (b == SERIAL_END_MARKER)
    {
      SERIAL_HAVE_DATA = true;
      break;
    }
  }

  if (!SERIAL_HAVE_DATA)
    return;

  /* Talk to IVAD */

  if (SERIAL_BUFFER[0] == SERIAL_START_IVAD)
  {
    if (SERIAL_BUFFER_DATA_SIZE != 5 && SERIAL_BUFFER[4] != SERIAL_END_MARKER)
    {
      serial_answer(SERIAL_START_IVAD, false);
      goto done;
    }

    byte addr = SERIAL_BUFFER[1];
    byte reg = SERIAL_BUFFER[2];
    byte val = SERIAL_BUFFER[3];

    ivad_write(addr, reg, val);

    serial_answer(SERIAL_START_IVAD, true);
    goto done;
  }

  /* Everything for EEPROM */

  if (SERIAL_BUFFER[0] == SERIAL_START_EEPROM)
  {
    // Read EEPROM
    if (SERIAL_BUFFER_DATA_SIZE == 2)
    {
      byte cfg[EEPROM_SLOTS];

      for (int i = 0; i < EEPROM_SLOTS; i++)
        cfg[i] = EEPROMwl.read(i);

      Serial.write(SERIAL_START_EEPROM);
      Serial.write(EEPROM_SLOTS);
      Serial.write(cfg, EEPROM_SLOTS);
      Serial.write(SERIAL_END_MARKER);
      goto done;
    }

    // Verify checksum
    if (SERIAL_BUFFER_DATA_SIZE == 3 && SERIAL_BUFFER[1] == SERIAL_EEPROM_VERFIY)
    {
      serial_answer(SERIAL_START_EEPROM, eeprom_verify_checksum());
      goto done;
    }

    // Reset EEPROM
    if (SERIAL_BUFFER_DATA_SIZE == 3 && SERIAL_BUFFER[1] == SERIAL_EEPROM_RESET)
    {
      eeprom_reset();
      serial_answer(SERIAL_START_EEPROM, true);
      goto done;
    }

    // Otherwise, write EEPROM
    byte len = SERIAL_BUFFER[1];
    if (SERIAL_BUFFER_DATA_SIZE < len || len != EEPROM_SLOTS)
    {
      serial_answer(SERIAL_START_EEPROM, false);
      goto done;
    }

    for (int i = 0; i < EEPROM_SLOTS; i++)
      EEPROMwl.update(i, SERIAL_BUFFER[2 + i]);

    serial_answer(SERIAL_START_EEPROM, true);
    goto done;
  }

done:
  SERIAL_HAVE_DATA = false;
  SERIAL_BUFFER_DATA_SIZE = 0;
}

void serial_answer(byte start_marker, bool ok)
{
  Serial.write(start_marker);
  Serial.write(ok ? SERIAL_OK_MARKER : SERIAL_ERROR_MARKER);
  Serial.write(SERIAL_END_MARKER);
}

void edid_on_request()
{
  Wire.write(EMAC_EDID_BIN, EMAC_EDID_BIN_LEN);
}

void ivad_write(byte address, byte val)
{
  IVAD_I2C_BUS.beginTransmission(address);
  IVAD_I2C_BUS.write(val);
  IVAD_I2C_BUS.endTransmission();
}

void ivad_write(byte address, byte reg, byte val)
{
  IVAD_I2C_BUS.beginTransmission(address);
  IVAD_I2C_BUS.write(reg);
  IVAD_I2C_BUS.write(val);
  IVAD_I2C_BUS.endTransmission();
}

void ivad_read(byte address, byte read_max)
{
  IVAD_I2C_BUS.requestFrom(address, read_max);

  while (IVAD_I2C_BUS.available())
    char c = IVAD_I2C_BUS.read();
}

void ivad_initialize()
{
  if (digitalRead(PIN_RELAY_CRT) != RELAY_ON)
    return;

  if (!eeprom_verify_checksum())
    eeprom_reset();

  ivad_write(IVAD_REGISTER_PROPERTY1, 0 , 0);

  for (int i = 0; i <= 100; i += 10)
  {
    ivad_write(0x53, i);
    ivad_read(0x53, 10);
  }

  ivad_write(IVAD_REGISTER_PROPERTY1, 0x01, EEPROMwl.read(EEPROM_R1_01));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x02, EEPROMwl.read(EEPROM_R1_02));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x03, EEPROMwl.read(EEPROM_R1_03));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x04, EEPROMwl.read(EEPROM_R1_04));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x05, EEPROMwl.read(EEPROM_R1_05));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x06, EEPROMwl.read(EEPROM_R1_06));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x07, EEPROMwl.read(EEPROM_R1_07));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x08, EEPROMwl.read(EEPROM_R1_08));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x09, EEPROMwl.read(EEPROM_R1_09));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0A, EEPROMwl.read(EEPROM_R1_0A));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0B, EEPROMwl.read(EEPROM_R1_0B));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0C, EEPROMwl.read(EEPROM_R1_0C));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0D, EEPROMwl.read(EEPROM_R1_0D));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0E, EEPROMwl.read(EEPROM_R1_0E));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x0F, EEPROMwl.read(EEPROM_R1_0F));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x10, EEPROMwl.read(EEPROM_R1_10));
  // Apple service source says IVAD controls the G2 voltage and there is
  // no G2 potentiometer on the CRT, so be careful:
  // 0x11 is probably G2 voltage, not brightness; 0x10 may be brightness!
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x11, EEPROMwl.read(EEPROM_R1_11));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x12, EEPROMwl.read(EEPROM_R1_12));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x14, EEPROMwl.read(EEPROM_R1_14));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x15, EEPROMwl.read(EEPROM_R1_15));
  ivad_write(IVAD_REGISTER_PROPERTY2, 0x00, EEPROMwl.read(EEPROM_R2_00));
  ivad_write(IVAD_REGISTER_PROPERTY2, 0x01, EEPROMwl.read(EEPROM_R2_01));
  ivad_write(IVAD_REGISTER_PROPERTY2, 0x02, EEPROMwl.read(EEPROM_R2_02));
  ivad_write(IVAD_REGISTER_PROPERTY2, 0x03, EEPROMwl.read(EEPROM_R2_03));
  ivad_write(IVAD_REGISTER_PROPERTY1, 0x00, EEPROMwl.read(EEPROM_R1_00));
}

void eeprom_reset()
{
  EEPROMwl.update(EEPROM_R1_01, 0xAE);
  EEPROMwl.update(EEPROM_R1_02, 0xAA);
  EEPROMwl.update(EEPROM_R1_03, 0xB6);
  EEPROMwl.update(EEPROM_R1_04, 0xBF);
  EEPROMwl.update(EEPROM_R1_05, 0xBC);
  EEPROMwl.update(EEPROM_R1_06, 0x3B);
  EEPROMwl.update(EEPROM_R1_07, 0xD0); // CE
  EEPROMwl.update(EEPROM_R1_08, 0xBC); // B9
  EEPROMwl.update(EEPROM_R1_09, 0xBB); // B3
  EEPROMwl.update(EEPROM_R1_0A, 0x96);
  EEPROMwl.update(EEPROM_R1_0B, 0xC0); // D3
  EEPROMwl.update(EEPROM_R1_0C, 0xDB);
  EEPROMwl.update(EEPROM_R1_0D, 0x18); // 1A
  EEPROMwl.update(EEPROM_R1_0E, 0xC5);
  EEPROMwl.update(EEPROM_R1_0F, 0xC2);
  EEPROMwl.update(EEPROM_R1_10, 0x9B); // 8A
  EEPROMwl.update(EEPROM_R1_11, 0x10); // 0B
  EEPROMwl.update(EEPROM_R1_12, 0x46);
  EEPROMwl.update(EEPROM_R1_14, 0x37);
  EEPROMwl.update(EEPROM_R1_15, 0x47); // 43
  EEPROMwl.update(EEPROM_R2_00, 0xD4);
  EEPROMwl.update(EEPROM_R2_01, 0xCE);
  EEPROMwl.update(EEPROM_R2_02, 0xCE);
  EEPROMwl.update(EEPROM_R2_03, 0x9D);
  EEPROMwl.update(EEPROM_R1_00, 0xF0); // FF

  int sum = 0;
  for (int i = 0; i < EEPROM_SLOTS - 1; i++)
    sum += EEPROMwl.read(i);
  sum = 256 - (sum % 256);
  if (sum == 0) sum = 1;
  
  EEPROMwl.update(EEPROM_CHECKSUM, sum);
}

bool eeprom_verify_checksum()
{
  byte expected = EEPROMwl.read(EEPROM_CHECKSUM);
  int sum = 0;
  for (int i = 0; i < EEPROM_SLOTS - 1; i++)
    sum += EEPROMwl.read(i);
  sum = 256 - (sum % 256);
  if (sum == 0) sum = 1;
  return sum == expected;
}
