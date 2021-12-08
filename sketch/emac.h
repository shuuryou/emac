#include <Wire.h>
#include <SoftwareWire.h>
#include <ezButton.h>
#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wunused-variable"
#include <EEPROMWearLevel.h>
#pragma GCC diagnostic pop

#define RELAY_OFF       HIGH
#define RELAY_ON        LOW

#define LED_OFF         LOW
#define LED_ON          HIGH

#define PIN_VGA_SDA       2 // Must be on Arduino SDA pin
#define PIN_VGA_SCL       3 // Must be on Arduino SCL pin
#define PIN_IVAD_SDA      4
#define PIN_IVAD_SCL      5
#define PIN_PWRBTN        9
#define PIN_PANDA_S3      10
#define PIN_PANDA_S4      11
#define PIN_PANDA_S5      12
#define PIN_FLED          LED_BUILTIN // TODO: PWMでフェード効果？
#define PIN_RELAY_BTN     21
#define PIN_RELAY_CRT     22
#define PIN_RELAY_AMP     23

// How long user must push eMac power button for ON/wakeup and force off
#define POWER_BUTTON_PUSH_DURATION_ONWAKE 250
#define POWER_BUTTON_PUSH_DURATION_OFF 3000

// How long relay must "push" Panda's power button to on/off or wake
#define POWER_BUTTON_RELAY_PUSH_ON_DELAY 3500
#define POWER_BUTTON_RELAY_PUSH_WAKE_DELAY 1000

// How quickly LED on-off cycle in S3 (sleep) or S4 (hibernate) mode
#define FLED_PANDA_S3_FLASHING_RATE 500
#define FLED_PANDA_S4_FLASHING_RATE 1000

// Rocky Hill uses 5000, lower works too?
#define IVAD_SETTLE_TIME 3000

// IVAD locations on I2C
#define IVAD_REGISTER_PROPERTY1  0x46
#define IVAD_REGISTER_PROPERTY2  0x4C

// For serial communications
#define SERIAL_START_IVAD     0xFC
#define SERIAL_START_EEPROM   0xEC
#define SERIAL_END_MARKER     0xFF
#define SERIAL_EEPROM_VERFIY  0xCC
#define SERIAL_EEPROM_RESET   0xDD
#define SERIAL_OK_MARKER      0x06
#define SERIAL_ERROR_MARKER   0x15
#define SERIAL_BUFFER_MAX_SIZE 64

// Power states of LattePanda Alpha board
#define PWRSTATE_DONTKNOW -1
#define PWRSTATE_OFF      0
#define PWRSTATE_POWERING 1
#define PWRSTATE_SLEEPING 2

// State of the eMac power button handling
#define PWRBTN_NONE        0
#define PWRBTN_IGNORE      1
#define PWRBTN_WAITRELEASE 2

// Original dumped eMac EDID
const byte EMAC_EDID_BIN[] = {
  0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x06, 0x10, 0x07, 0x9d,
  0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x01, 0x03, 0x68, 0x21, 0x18, 0x6f,
  0x08, 0xf6, 0x29, 0xa2, 0x53, 0x47, 0x99, 0x25, 0x10, 0x48, 0x4c, 0x00,
  0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
  0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0xbf, 0x26, 0x00, 0x60, 0x41, 0x00,
  0x2a, 0x30, 0x30, 0x60, 0x13, 0x00, 0x40, 0xf0, 0x10, 0x00, 0x00, 0x1e,
  0xc0, 0x2f, 0x00, 0xa0, 0x51, 0xc0, 0x2a, 0x30, 0x30, 0x60, 0x13, 0x00,
  0x40, 0xf0, 0x10, 0x00, 0x00, 0x1e, 0x00, 0x00, 0x00, 0xfd, 0x00, 0x46,
  0x8c, 0x47, 0x49, 0x0d, 0x00, 0x0a, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
  0x00, 0x00, 0x00, 0xfc, 0x00, 0x69, 0x4d, 0x61, 0x63, 0x0a, 0x20, 0x20,
  0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00, 0xcd
};
#define EMAC_EDID_BIN_LEN 128

// For EEPROM
#define EEPROM_SLOTS 26
#define EEPROM_VERSION 1
#define EEPROM_R1_01 0
#define EEPROM_R1_02 1
#define EEPROM_R1_03 2
#define EEPROM_R1_04 3
#define EEPROM_R1_05 4
#define EEPROM_R1_06 5
#define EEPROM_R1_07 6
#define EEPROM_R1_08 7
#define EEPROM_R1_09 8
#define EEPROM_R1_0A 9
#define EEPROM_R1_0B 10
#define EEPROM_R1_0C 11
#define EEPROM_R1_0D 12
#define EEPROM_R1_0E 13
#define EEPROM_R1_0F 14
#define EEPROM_R1_10 15
#define EEPROM_R1_11 16
#define EEPROM_R1_12 17
#define EEPROM_R1_14 18
#define EEPROM_R1_15 19
#define EEPROM_R2_00 20
#define EEPROM_R2_01 21
#define EEPROM_R2_02 22
#define EEPROM_R2_03 23
#define EEPROM_R1_00 24
#define EEPROM_CHECKSUM 25

#if BUFFER_LENGTH < EMAC_EDID_BIN_LEN
#error Must patch the Wire library to send EDID correctly.
#endif
