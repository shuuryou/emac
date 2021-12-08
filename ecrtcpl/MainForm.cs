using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ecrtcpl
{
    public partial class MainForm : Form
    {
        // Set this to false if you don't have an Arduino Leonardo
        // or LattePanda Alpha (ATmega32U4 boards). They need DTR
        // or won't send data.
        // NOTE: Other boards use DTR as a reset line...
        private const bool NEEDS_DTR = true;

        private const byte SERIAL_START_IVAD = 0xFC;
        private const byte SERIAL_START_EEPROM = 0xEC;
        private const byte SERIAL_END_MARKER = 0xFF;
        private const byte SERIAL_EEPROM_VERFIY = 0xCC;
        private const byte SERIAL_EEPROM_RESET = 0xDD;
        private const byte SERIAL_OK_MARKER = 0x06;
        private const byte SERIAL_ERROR_MARKER = 0x15;

        private struct Aspect
        {
            public Aspect(byte addr, byte prop, byte eepromidx, string desc)
            {
                Address = addr;
                Property = prop;
                EepromIndex = eepromidx;
                Description = desc;
            }

            public byte Address { get; set; }

            public byte Property { get; set; }

            public byte EepromIndex { get; set; }

            public string Description { get; set; }

            public override string ToString()
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "{0:X2}h/{1:X2}h: {2}",
                    Address, Property, Description);
            }
        }

        private byte[] SETTINGS = new byte[26];

        public MainForm()
        {
            InitializeComponent();

            serialPort.DtrEnable = NEEDS_DTR;

            aspectComboBox.Items.Add(new Aspect(0x46, 0x00, 24, "Contrast"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x01, 0, "G Drive"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x02, 1, "B Drive"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x03, 2, "R Drive"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x04, 3, "T Pinch"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x05, 4, "T Lean"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x06, 5, "B Lean"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x07, 6, "H Pos"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x08, 7, "V Size"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x09, 8, "V Pos"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0A, 9, "Trapezoid"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0B, 10, "Keystone"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0C, 11, "Pincushion"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0D, 12, "H Size"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0E, 13, "T&B Bow"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x0F, 14, "Paralellogram"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x10, 15, "Brightness???"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x11, 16, "G2??? (DANGEROUS)"));
            aspectComboBox.Items.Add(new Aspect(0x46, 0x15, 19, "B Pinch"));
            aspectComboBox.Items.Add(new Aspect(0x4C, 0x00, 20, "R Cutoff"));
            aspectComboBox.Items.Add(new Aspect(0x4C, 0x01, 21, "G Cutoff"));
            aspectComboBox.Items.Add(new Aspect(0x4C, 0x02, 22, "B Cutoff"));
            aspectComboBox.Items.Add(new Aspect(0x4C, 0x03, 23, "Rotation"));

            aspectComboBox.SelectedIndex = 0;

            i2cUpDown.Font = propertyUpDown.Font = ivadValueUpDown.Font =
                aspectValueUpDown.Font = currentEepromTextBox.Font =
                newEepromTextBox.Font = eepromOffsetUpDown.Font =
                eepromValueUpDown.Font = new Font(FontFamily.GenericMonospace, this.Font.Size);

            controlPortComboBox.Items.AddRange(SerialPort.GetPortNames());

            eepromOffsetUpDown.Maximum = SETTINGS.Length - 1;

            UpdateNewEeprom();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }

        private void controlPortComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ivadGroupBox.Enabled = autoApplyCheckBox.Enabled = aspectButton.Enabled =
                currentEepromGroupBox.Enabled = verifyChecksumButton.Enabled =
                defaultsButton.Enabled = readEepromButton.Enabled =
                writeEepromButton.Enabled = false;

            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                serialPort.PortName = controlPortComboBox.Text;
                serialPort.Open();
            }
            catch (UnauthorizedAccessException ex)
            {
                string msg = string.Format(CultureInfo.CurrentUICulture, Resources.ErrorOpenSerialPort, ex.Message);
                MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (IOException ex)
            {
                string msg = string.Format(CultureInfo.CurrentUICulture, Resources.ErrorOpenSerialPort, ex.Message);
                MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ivadGroupBox.Enabled = autoApplyCheckBox.Enabled = aspectButton.Enabled =
                currentEepromGroupBox.Enabled = verifyChecksumButton.Enabled =
                defaultsButton.Enabled = readEepromButton.Enabled =
                writeEepromButton.Enabled = true;
        }

        private void ivadButton_Click(object sender, EventArgs e)
        {
            string msg = string.Format(CultureInfo.CurrentUICulture, Resources.IvadDirectIsDangerous,
                (int)propertyUpDown.Value, (int)ivadValueUpDown.Value, (int)i2cUpDown.Value);

            if (MessageBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            byte[] req = new byte[] { SERIAL_START_IVAD,
                (byte)i2cUpDown.Value,
                (byte)propertyUpDown.Value,
                (byte)ivadValueUpDown.Value,
                SERIAL_END_MARKER };
            byte[] response;

            try
            {
                response = controller_communicate(req);
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response.Length != 3 || response[0] != SERIAL_START_IVAD || response[1] != SERIAL_OK_MARKER)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void aspectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aspectComboBox.SelectedIndex == -1)
            {
                aspectValueUpDown.Value = 0;
                aspectValueUpDown.Enabled = false;
                return;
            }
            else
            {
                aspectValueUpDown.Value = SETTINGS[((Aspect)aspectComboBox.SelectedItem).EepromIndex];
                aspectValueUpDown.Enabled = true;
            }
        }

        private void aspectButton_Click(object sender, EventArgs e)
        {
            Aspect a = (Aspect)aspectComboBox.SelectedItem;

            byte[] req = new byte[] { SERIAL_START_IVAD, a.Address, a.Property,
                (byte)aspectValueUpDown.Value , SERIAL_END_MARKER };
            byte[] response;

            try
            {
                response = controller_communicate(req);
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response.Length != 3 || response[0] != SERIAL_START_IVAD || response[1] != SERIAL_OK_MARKER)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void autoApplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            aspectButton.Enabled = !autoApplyCheckBox.Checked;
        }

        private void aspectValueUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (aspectComboBox.SelectedIndex == -1)
                return;

            SETTINGS[((Aspect)aspectComboBox.SelectedItem).EepromIndex] = (byte)aspectValueUpDown.Value;

            eepromOffsetUpDown_ValueChanged(null, EventArgs.Empty);
            UpdateNewEeprom();

            if (autoApplyCheckBox.Checked)
                aspectButton_Click(null, EventArgs.Empty);
        }

        private void UpdateNewEeprom()
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in SETTINGS)
                sb.AppendFormat("{0:X2} ", b);

            newEepromTextBox.Text = sb.ToString().TrimEnd();
        }

        private void readEepromButton_Click(object sender, EventArgs e)
        {
            byte[] req = new byte[] { SERIAL_START_EEPROM, SERIAL_END_MARKER };
            byte[] response;

            try
            {
                response = controller_communicate(req);
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response[0] != SERIAL_START_EEPROM || response[response.Length - 1] != SERIAL_END_MARKER)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.Length - 3 != response[1] || response.Length - 3 != SETTINGS.Length)
            {
                MessageBox.Show(this, Resources.BadEepromLength, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 2; i < response.Length - 1; i++)
            {
                SETTINGS[i - 2] = response[i];
                sb.AppendFormat("{0:X2} ", response[i]);
            }

            currentEepromTextBox.Text = sb.ToString().TrimEnd();

            if (MessageBox.Show(this, Resources.ConfirmUseReadAsNew, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            for (int i = 2; i < response.Length - 1; i++)
                SETTINGS[i - 2] = response[i];

            eepromOffsetUpDown_ValueChanged(null, EventArgs.Empty);
            UpdateNewEeprom();
        }

        private void verifyChecksumButton_Click(object sender, EventArgs e)
        {
            byte[] req = new byte[] { SERIAL_START_EEPROM, SERIAL_EEPROM_VERFIY, SERIAL_END_MARKER };
            byte[] response;

            try
            {
                response = controller_communicate(req);
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response.Length != 3 || response[0] != SERIAL_START_EEPROM)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response[1] == SERIAL_OK_MARKER)
                MessageBox.Show(this, Resources.ChecksumIsOk, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, Resources.ChecksumIsBad, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void defaultsButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Resources.ConfirmReset, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            byte[] req = new byte[] { SERIAL_START_EEPROM, SERIAL_EEPROM_RESET, SERIAL_END_MARKER };
            byte[] response;

            try
            {
                response = controller_communicate(req);
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response.Length != 3 || response[0] != SERIAL_START_EEPROM || response[1] != SERIAL_OK_MARKER)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(this, Resources.ConfirmReload, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            aspectComboBox.SelectedIndex = -1;
            readEepromButton_Click(null, EventArgs.Empty);
        }

        private void writeEepromButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Resources.ConfirmWriteEeprom, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            byte sum = calc_checksum(SETTINGS, 0, SETTINGS.Length - 1);
            if (sum != SETTINGS[SETTINGS.Length - 1])
            {
                string msg = string.Format(CultureInfo.CurrentUICulture, Resources.FixBadChecksum,
                    sum, SETTINGS[SETTINGS.Length - 1]);

                if (MessageBox.Show(this, msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    SETTINGS[SETTINGS.Length - 1] = sum;
                    eepromOffsetUpDown_ValueChanged(null, EventArgs.Empty);
                    UpdateNewEeprom();
                }
            }

            List<byte> req = new List<byte>();
            req.Add(SERIAL_START_EEPROM);
            req.Add((byte)SETTINGS.Length);
            req.AddRange(SETTINGS);
            req.Add(SERIAL_END_MARKER);

            byte[] response;

            try
            {
                response = controller_communicate(req.ToArray());
            }
            catch (ControllerException ex)
            {
                ShowControllerError(ex);
                return;
            }

            if (response.Length != 3 || response[0] != SERIAL_START_EEPROM || response[1] != SERIAL_OK_MARKER)
            {
                MessageBox.Show(this, Resources.BadEepromResponse, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(this, Resources.WriteSuccess, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show(this, Resources.ConfirmReload, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            aspectComboBox.SelectedIndex = -1;
            readEepromButton_Click(null, EventArgs.Empty);
        }

        private void eepromOffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            eepromValueUpDown.Value = SETTINGS[(int)eepromOffsetUpDown.Value];
            autoChecksumCheckBox.Enabled = (eepromOffsetUpDown.Value != SETTINGS.Length - 1);
        }

        private void eepromValueUpDown_ValueChanged(object sender, EventArgs e)
        {
            SETTINGS[(int)eepromOffsetUpDown.Value] = (byte)eepromValueUpDown.Value;

            if (autoChecksumCheckBox.Enabled && autoChecksumCheckBox.Checked)
                SETTINGS[SETTINGS.Length - 1] = calc_checksum(SETTINGS, 0, SETTINGS.Length - 1);

            UpdateNewEeprom();
        }

        private void ShowControllerError(Exception ex)
        {
            // TODO: Better error display

            string msg = string.Format(CultureInfo.CurrentUICulture,
                    Resources.ControllerError, ex);

            MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static byte calc_checksum(byte[] arr, int offset, int length)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));

            if (offset < 0 || offset > arr.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (length <= 0 || offset + length > arr.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            int sum = 0;

            for (int i = offset; i < offset + length; i++)
                sum += arr[i];

            byte ret = (byte)(256 - (sum % 256));

            if (ret == 0) ret = 1; // Checksum may never be 0

            return ret;
        }

        [Serializable]
        public class ControllerException : Exception
        {
            public ControllerException() { }
            public ControllerException(string message) : base(message) { }
            public ControllerException(string message, Exception inner) : base(message, inner) { }
            protected ControllerException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        private byte[] controller_communicate(byte[] req)
        {
            if (!serialPort.IsOpen)
                throw new ControllerException("Serial port is not opened.");

            int tries = 0;

        again:
            tries++;

            if (tries > 3)
                throw new ControllerException("No response from controller.");

            serialPort.Write(req, 0, req.Length);

            byte[] response = new byte[255];
            int readsofar = 0;

            try
            {
                for (int iterations = 0; ; iterations++)
                {
                    int read = serialPort.BaseStream.Read(response, readsofar, response.Length - readsofar);
                    readsofar += read;

                    if (readsofar == 0)
                        goto again;

                    if (readsofar < 3)
                        continue;

                    if (response[readsofar - 1] != SERIAL_END_MARKER)
                        continue;

                    if (iterations > 1000)
                        throw new ControllerException("Infinite loop detected in read loop.");
                    break;
                }
            }
            catch (IOException ex)
            {
                throw new ControllerException("I/O error while reading serial port.", ex);
            }
            catch (TimeoutException)
            {
                goto again;
            }

            Array.Resize(ref response, readsofar);
            return response;
        }
    }
}