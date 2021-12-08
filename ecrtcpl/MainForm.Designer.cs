
namespace ecrtcpl
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ivadGroupBox = new System.Windows.Forms.GroupBox();
            this.ivadTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.i2cUpDown = new System.Windows.Forms.NumericUpDown();
            this.propertyUpDown = new System.Windows.Forms.NumericUpDown();
            this.ivadValueUpDown = new System.Windows.Forms.NumericUpDown();
            this.i2cLabel = new System.Windows.Forms.Label();
            this.propertyLabel = new System.Windows.Forms.Label();
            this.ivadValueLabel = new System.Windows.Forms.Label();
            this.ivadButton = new System.Windows.Forms.Button();
            this.aspectGroupBox = new System.Windows.Forms.GroupBox();
            this.aspectTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.aspectValueUpDown = new System.Windows.Forms.NumericUpDown();
            this.aspectLabel = new System.Windows.Forms.Label();
            this.aspectValueLabel = new System.Windows.Forms.Label();
            this.aspectComboBox = new System.Windows.Forms.ComboBox();
            this.aspectButton = new System.Windows.Forms.Button();
            this.autoApplyCheckBox = new System.Windows.Forms.CheckBox();
            this.currentEepromGroupBox = new System.Windows.Forms.GroupBox();
            this.currentEepromTextBox = new System.Windows.Forms.TextBox();
            this.newEepromGroupBox = new System.Windows.Forms.GroupBox();
            this.newEepromTextBox = new System.Windows.Forms.TextBox();
            this.writeEepromButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.controlPortGroupBox = new System.Windows.Forms.GroupBox();
            this.controlPortComboBox = new System.Windows.Forms.ComboBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.readEepromButton = new System.Windows.Forms.Button();
            this.eepromEditorGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.eepromValueUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eepromOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.autoChecksumCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultsButton = new System.Windows.Forms.Button();
            this.verifyChecksumButton = new System.Windows.Forms.Button();
            this.ivadGroupBox.SuspendLayout();
            this.ivadTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.i2cUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivadValueUpDown)).BeginInit();
            this.aspectGroupBox.SuspendLayout();
            this.aspectTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aspectValueUpDown)).BeginInit();
            this.currentEepromGroupBox.SuspendLayout();
            this.newEepromGroupBox.SuspendLayout();
            this.controlPortGroupBox.SuspendLayout();
            this.eepromEditorGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eepromValueUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eepromOffsetUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ivadGroupBox
            // 
            this.ivadGroupBox.Controls.Add(this.ivadTableLayoutPanel);
            this.ivadGroupBox.Enabled = false;
            this.ivadGroupBox.Location = new System.Drawing.Point(8, 64);
            this.ivadGroupBox.Name = "ivadGroupBox";
            this.ivadGroupBox.Size = new System.Drawing.Size(224, 128);
            this.ivadGroupBox.TabIndex = 1;
            this.ivadGroupBox.TabStop = false;
            this.ivadGroupBox.Text = "IVAD I²C Bus Write (DANGER)";
            // 
            // ivadTableLayoutPanel
            // 
            this.ivadTableLayoutPanel.ColumnCount = 2;
            this.ivadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ivadTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ivadTableLayoutPanel.Controls.Add(this.i2cUpDown, 1, 0);
            this.ivadTableLayoutPanel.Controls.Add(this.propertyUpDown, 1, 1);
            this.ivadTableLayoutPanel.Controls.Add(this.ivadValueUpDown, 1, 2);
            this.ivadTableLayoutPanel.Controls.Add(this.i2cLabel, 0, 0);
            this.ivadTableLayoutPanel.Controls.Add(this.propertyLabel, 0, 1);
            this.ivadTableLayoutPanel.Controls.Add(this.ivadValueLabel, 0, 2);
            this.ivadTableLayoutPanel.Controls.Add(this.ivadButton, 1, 3);
            this.ivadTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivadTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.ivadTableLayoutPanel.Name = "ivadTableLayoutPanel";
            this.ivadTableLayoutPanel.RowCount = 5;
            this.ivadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ivadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ivadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ivadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ivadTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ivadTableLayoutPanel.Size = new System.Drawing.Size(218, 109);
            this.ivadTableLayoutPanel.TabIndex = 0;
            // 
            // i2cUpDown
            // 
            this.i2cUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.i2cUpDown.Hexadecimal = true;
            this.i2cUpDown.Location = new System.Drawing.Point(58, 3);
            this.i2cUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.i2cUpDown.Name = "i2cUpDown";
            this.i2cUpDown.Size = new System.Drawing.Size(157, 20);
            this.i2cUpDown.TabIndex = 1;
            // 
            // propertyUpDown
            // 
            this.propertyUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyUpDown.Hexadecimal = true;
            this.propertyUpDown.Location = new System.Drawing.Point(58, 29);
            this.propertyUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.propertyUpDown.Name = "propertyUpDown";
            this.propertyUpDown.Size = new System.Drawing.Size(157, 20);
            this.propertyUpDown.TabIndex = 3;
            // 
            // ivadValueUpDown
            // 
            this.ivadValueUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ivadValueUpDown.Hexadecimal = true;
            this.ivadValueUpDown.Location = new System.Drawing.Point(58, 55);
            this.ivadValueUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ivadValueUpDown.Name = "ivadValueUpDown";
            this.ivadValueUpDown.Size = new System.Drawing.Size(157, 20);
            this.ivadValueUpDown.TabIndex = 5;
            // 
            // i2cLabel
            // 
            this.i2cLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.i2cLabel.AutoSize = true;
            this.i2cLabel.Location = new System.Drawing.Point(3, 6);
            this.i2cLabel.Name = "i2cLabel";
            this.i2cLabel.Size = new System.Drawing.Size(48, 13);
            this.i2cLabel.TabIndex = 0;
            this.i2cLabel.Text = "&Address:";
            // 
            // propertyLabel
            // 
            this.propertyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.propertyLabel.AutoSize = true;
            this.propertyLabel.Location = new System.Drawing.Point(3, 32);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(49, 13);
            this.propertyLabel.TabIndex = 2;
            this.propertyLabel.Text = "&Property:";
            // 
            // ivadValueLabel
            // 
            this.ivadValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ivadValueLabel.AutoSize = true;
            this.ivadValueLabel.Location = new System.Drawing.Point(3, 58);
            this.ivadValueLabel.Name = "ivadValueLabel";
            this.ivadValueLabel.Size = new System.Drawing.Size(37, 13);
            this.ivadValueLabel.TabIndex = 4;
            this.ivadValueLabel.Text = "&Value:";
            // 
            // ivadButton
            // 
            this.ivadButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ivadButton.Location = new System.Drawing.Point(140, 81);
            this.ivadButton.Name = "ivadButton";
            this.ivadButton.Size = new System.Drawing.Size(75, 23);
            this.ivadButton.TabIndex = 6;
            this.ivadButton.Text = "&Execute";
            this.ivadButton.UseVisualStyleBackColor = true;
            this.ivadButton.Click += new System.EventHandler(this.ivadButton_Click);
            // 
            // aspectGroupBox
            // 
            this.aspectGroupBox.Controls.Add(this.aspectTableLayoutPanel);
            this.aspectGroupBox.Location = new System.Drawing.Point(8, 200);
            this.aspectGroupBox.Name = "aspectGroupBox";
            this.aspectGroupBox.Size = new System.Drawing.Size(224, 104);
            this.aspectGroupBox.TabIndex = 2;
            this.aspectGroupBox.TabStop = false;
            this.aspectGroupBox.Text = "Basic Adjustment (less dangeorus)";
            // 
            // aspectTableLayoutPanel
            // 
            this.aspectTableLayoutPanel.ColumnCount = 3;
            this.aspectTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.aspectTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.aspectTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.aspectTableLayoutPanel.Controls.Add(this.aspectValueUpDown, 1, 1);
            this.aspectTableLayoutPanel.Controls.Add(this.aspectLabel, 0, 0);
            this.aspectTableLayoutPanel.Controls.Add(this.aspectValueLabel, 0, 1);
            this.aspectTableLayoutPanel.Controls.Add(this.aspectComboBox, 1, 0);
            this.aspectTableLayoutPanel.Controls.Add(this.aspectButton, 2, 3);
            this.aspectTableLayoutPanel.Controls.Add(this.autoApplyCheckBox, 0, 3);
            this.aspectTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aspectTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.aspectTableLayoutPanel.Name = "aspectTableLayoutPanel";
            this.aspectTableLayoutPanel.RowCount = 4;
            this.aspectTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aspectTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aspectTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.aspectTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.aspectTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.aspectTableLayoutPanel.Size = new System.Drawing.Size(218, 85);
            this.aspectTableLayoutPanel.TabIndex = 0;
            // 
            // aspectValueUpDown
            // 
            this.aspectValueUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.aspectTableLayoutPanel.SetColumnSpan(this.aspectValueUpDown, 2);
            this.aspectValueUpDown.Enabled = false;
            this.aspectValueUpDown.Hexadecimal = true;
            this.aspectValueUpDown.Location = new System.Drawing.Point(52, 30);
            this.aspectValueUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.aspectValueUpDown.Name = "aspectValueUpDown";
            this.aspectValueUpDown.Size = new System.Drawing.Size(163, 20);
            this.aspectValueUpDown.TabIndex = 3;
            this.aspectValueUpDown.ValueChanged += new System.EventHandler(this.aspectValueUpDown_ValueChanged);
            // 
            // aspectLabel
            // 
            this.aspectLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.aspectLabel.AutoSize = true;
            this.aspectLabel.Location = new System.Drawing.Point(3, 7);
            this.aspectLabel.Name = "aspectLabel";
            this.aspectLabel.Size = new System.Drawing.Size(43, 13);
            this.aspectLabel.TabIndex = 0;
            this.aspectLabel.Text = "&Setting:";
            // 
            // aspectValueLabel
            // 
            this.aspectValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.aspectValueLabel.AutoSize = true;
            this.aspectValueLabel.Location = new System.Drawing.Point(3, 33);
            this.aspectValueLabel.Name = "aspectValueLabel";
            this.aspectValueLabel.Size = new System.Drawing.Size(37, 13);
            this.aspectValueLabel.TabIndex = 2;
            this.aspectValueLabel.Text = "Va&lue:";
            // 
            // aspectComboBox
            // 
            this.aspectComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.aspectTableLayoutPanel.SetColumnSpan(this.aspectComboBox, 2);
            this.aspectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aspectComboBox.FormattingEnabled = true;
            this.aspectComboBox.Location = new System.Drawing.Point(52, 3);
            this.aspectComboBox.Name = "aspectComboBox";
            this.aspectComboBox.Size = new System.Drawing.Size(163, 21);
            this.aspectComboBox.TabIndex = 1;
            this.aspectComboBox.SelectedIndexChanged += new System.EventHandler(this.aspectComboBox_SelectedIndexChanged);
            // 
            // aspectButton
            // 
            this.aspectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.aspectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.aspectButton.Enabled = false;
            this.aspectButton.Location = new System.Drawing.Point(140, 57);
            this.aspectButton.Name = "aspectButton";
            this.aspectButton.Size = new System.Drawing.Size(75, 23);
            this.aspectButton.TabIndex = 5;
            this.aspectButton.Text = "E&xecute";
            this.aspectButton.UseVisualStyleBackColor = true;
            this.aspectButton.Click += new System.EventHandler(this.aspectButton_Click);
            // 
            // autoApplyCheckBox
            // 
            this.autoApplyCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autoApplyCheckBox.AutoSize = true;
            this.aspectTableLayoutPanel.SetColumnSpan(this.autoApplyCheckBox, 2);
            this.autoApplyCheckBox.Enabled = false;
            this.autoApplyCheckBox.Location = new System.Drawing.Point(3, 60);
            this.autoApplyCheckBox.Name = "autoApplyCheckBox";
            this.autoApplyCheckBox.Size = new System.Drawing.Size(131, 17);
            this.autoApplyCheckBox.TabIndex = 4;
            this.autoApplyCheckBox.Text = "Au&to exec. on change";
            this.autoApplyCheckBox.UseVisualStyleBackColor = true;
            this.autoApplyCheckBox.CheckedChanged += new System.EventHandler(this.autoApplyCheckBox_CheckedChanged);
            // 
            // currentEepromGroupBox
            // 
            this.currentEepromGroupBox.Controls.Add(this.currentEepromTextBox);
            this.currentEepromGroupBox.Enabled = false;
            this.currentEepromGroupBox.Location = new System.Drawing.Point(240, 8);
            this.currentEepromGroupBox.Name = "currentEepromGroupBox";
            this.currentEepromGroupBox.Size = new System.Drawing.Size(336, 72);
            this.currentEepromGroupBox.TabIndex = 3;
            this.currentEepromGroupBox.TabStop = false;
            this.currentEepromGroupBox.Text = "Current EEPROM Content";
            // 
            // currentEepromTextBox
            // 
            this.currentEepromTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentEepromTextBox.Location = new System.Drawing.Point(3, 16);
            this.currentEepromTextBox.Multiline = true;
            this.currentEepromTextBox.Name = "currentEepromTextBox";
            this.currentEepromTextBox.ReadOnly = true;
            this.currentEepromTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.currentEepromTextBox.Size = new System.Drawing.Size(330, 53);
            this.currentEepromTextBox.TabIndex = 0;
            this.currentEepromTextBox.Text = "Click \"Read EEPROM\" to show it!";
            // 
            // newEepromGroupBox
            // 
            this.newEepromGroupBox.Controls.Add(this.newEepromTextBox);
            this.newEepromGroupBox.Location = new System.Drawing.Point(240, 120);
            this.newEepromGroupBox.Name = "newEepromGroupBox";
            this.newEepromGroupBox.Size = new System.Drawing.Size(336, 72);
            this.newEepromGroupBox.TabIndex = 7;
            this.newEepromGroupBox.TabStop = false;
            this.newEepromGroupBox.Text = "Proposed New EEPROM Content";
            // 
            // newEepromTextBox
            // 
            this.newEepromTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newEepromTextBox.Location = new System.Drawing.Point(3, 16);
            this.newEepromTextBox.Multiline = true;
            this.newEepromTextBox.Name = "newEepromTextBox";
            this.newEepromTextBox.ReadOnly = true;
            this.newEepromTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newEepromTextBox.Size = new System.Drawing.Size(330, 53);
            this.newEepromTextBox.TabIndex = 0;
            // 
            // writeEepromButton
            // 
            this.writeEepromButton.Enabled = false;
            this.writeEepromButton.Location = new System.Drawing.Point(472, 192);
            this.writeEepromButton.Name = "writeEepromButton";
            this.writeEepromButton.Size = new System.Drawing.Size(104, 24);
            this.writeEepromButton.TabIndex = 8;
            this.writeEepromButton.Text = "&Write EEPROM";
            this.writeEepromButton.UseVisualStyleBackColor = true;
            this.writeEepromButton.Click += new System.EventHandler(this.writeEepromButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exitButton.Location = new System.Drawing.Point(8, 312);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 10;
            this.exitButton.Text = "Exit (ESC)";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // controlPortGroupBox
            // 
            this.controlPortGroupBox.Controls.Add(this.controlPortComboBox);
            this.controlPortGroupBox.Location = new System.Drawing.Point(8, 8);
            this.controlPortGroupBox.Name = "controlPortGroupBox";
            this.controlPortGroupBox.Size = new System.Drawing.Size(224, 48);
            this.controlPortGroupBox.TabIndex = 0;
            this.controlPortGroupBox.TabStop = false;
            this.controlPortGroupBox.Text = "Select Control Port";
            // 
            // controlPortComboBox
            // 
            this.controlPortComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controlPortComboBox.FormattingEnabled = true;
            this.controlPortComboBox.Location = new System.Drawing.Point(3, 16);
            this.controlPortComboBox.Name = "controlPortComboBox";
            this.controlPortComboBox.Size = new System.Drawing.Size(218, 21);
            this.controlPortComboBox.TabIndex = 0;
            this.controlPortComboBox.SelectionChangeCommitted += new System.EventHandler(this.controlPortComboBox_SelectionChangeCommitted);
            // 
            // serialPort
            // 
            this.serialPort.DtrEnable = true;
            this.serialPort.ReadTimeout = 1000;
            this.serialPort.WriteTimeout = 1000;
            // 
            // readEepromButton
            // 
            this.readEepromButton.Enabled = false;
            this.readEepromButton.Location = new System.Drawing.Point(472, 80);
            this.readEepromButton.Name = "readEepromButton";
            this.readEepromButton.Size = new System.Drawing.Size(104, 24);
            this.readEepromButton.TabIndex = 6;
            this.readEepromButton.Text = "&Read EEPROM";
            this.readEepromButton.UseVisualStyleBackColor = true;
            this.readEepromButton.Click += new System.EventHandler(this.readEepromButton_Click);
            // 
            // eepromEditorGroupBox
            // 
            this.eepromEditorGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.eepromEditorGroupBox.Location = new System.Drawing.Point(240, 232);
            this.eepromEditorGroupBox.Name = "eepromEditorGroupBox";
            this.eepromEditorGroupBox.Size = new System.Drawing.Size(336, 72);
            this.eepromEditorGroupBox.TabIndex = 9;
            this.eepromEditorGroupBox.TabStop = false;
            this.eepromEditorGroupBox.Text = "EEPROM Direct Editor (DANGER)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.eepromValueUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.eepromOffsetUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.autoChecksumCheckBox, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(330, 53);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // eepromValueUpDown
            // 
            this.eepromValueUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.eepromValueUpDown.Hexadecimal = true;
            this.eepromValueUpDown.Location = new System.Drawing.Point(47, 29);
            this.eepromValueUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.eepromValueUpDown.Name = "eepromValueUpDown";
            this.eepromValueUpDown.Size = new System.Drawing.Size(174, 20);
            this.eepromValueUpDown.TabIndex = 3;
            this.eepromValueUpDown.ValueChanged += new System.EventHandler(this.eepromValueUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Offset:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Val&ue:";
            // 
            // eepromOffsetUpDown
            // 
            this.eepromOffsetUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.eepromOffsetUpDown.Hexadecimal = true;
            this.eepromOffsetUpDown.Location = new System.Drawing.Point(47, 3);
            this.eepromOffsetUpDown.Name = "eepromOffsetUpDown";
            this.eepromOffsetUpDown.Size = new System.Drawing.Size(174, 20);
            this.eepromOffsetUpDown.TabIndex = 1;
            this.eepromOffsetUpDown.ValueChanged += new System.EventHandler(this.eepromOffsetUpDown_ValueChanged);
            // 
            // autoChecksumCheckBox
            // 
            this.autoChecksumCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.autoChecksumCheckBox.AutoSize = true;
            this.autoChecksumCheckBox.Checked = true;
            this.autoChecksumCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoChecksumCheckBox.Location = new System.Drawing.Point(227, 30);
            this.autoChecksumCheckBox.Name = "autoChecksumCheckBox";
            this.autoChecksumCheckBox.Size = new System.Drawing.Size(100, 17);
            this.autoChecksumCheckBox.TabIndex = 4;
            this.autoChecksumCheckBox.Text = "Auto checksum";
            this.autoChecksumCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultsButton
            // 
            this.defaultsButton.Enabled = false;
            this.defaultsButton.Location = new System.Drawing.Point(352, 80);
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.Size = new System.Drawing.Size(72, 24);
            this.defaultsButton.TabIndex = 5;
            this.defaultsButton.Text = "&Defaults";
            this.defaultsButton.UseVisualStyleBackColor = true;
            this.defaultsButton.Click += new System.EventHandler(this.defaultsButton_Click);
            // 
            // verifyChecksumButton
            // 
            this.verifyChecksumButton.Enabled = false;
            this.verifyChecksumButton.Location = new System.Drawing.Point(240, 80);
            this.verifyChecksumButton.Name = "verifyChecksumButton";
            this.verifyChecksumButton.Size = new System.Drawing.Size(104, 24);
            this.verifyChecksumButton.TabIndex = 4;
            this.verifyChecksumButton.Text = "Verify chec&ksum";
            this.verifyChecksumButton.UseVisualStyleBackColor = true;
            this.verifyChecksumButton.Click += new System.EventHandler(this.verifyChecksumButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.exitButton;
            this.ClientSize = new System.Drawing.Size(584, 340);
            this.Controls.Add(this.verifyChecksumButton);
            this.Controls.Add(this.defaultsButton);
            this.Controls.Add(this.eepromEditorGroupBox);
            this.Controls.Add(this.readEepromButton);
            this.Controls.Add(this.controlPortGroupBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.writeEepromButton);
            this.Controls.Add(this.newEepromGroupBox);
            this.Controls.Add(this.currentEepromGroupBox);
            this.Controls.Add(this.aspectGroupBox);
            this.Controls.Add(this.ivadGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eMac CRT Controller Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ivadGroupBox.ResumeLayout(false);
            this.ivadTableLayoutPanel.ResumeLayout(false);
            this.ivadTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.i2cUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ivadValueUpDown)).EndInit();
            this.aspectGroupBox.ResumeLayout(false);
            this.aspectTableLayoutPanel.ResumeLayout(false);
            this.aspectTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aspectValueUpDown)).EndInit();
            this.currentEepromGroupBox.ResumeLayout(false);
            this.currentEepromGroupBox.PerformLayout();
            this.newEepromGroupBox.ResumeLayout(false);
            this.newEepromGroupBox.PerformLayout();
            this.controlPortGroupBox.ResumeLayout(false);
            this.eepromEditorGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eepromValueUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eepromOffsetUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ivadGroupBox;
        private System.Windows.Forms.TableLayoutPanel ivadTableLayoutPanel;
        private System.Windows.Forms.NumericUpDown i2cUpDown;
        private System.Windows.Forms.NumericUpDown propertyUpDown;
        private System.Windows.Forms.NumericUpDown ivadValueUpDown;
        private System.Windows.Forms.Label i2cLabel;
        private System.Windows.Forms.Label propertyLabel;
        private System.Windows.Forms.Label ivadValueLabel;
        private System.Windows.Forms.Button ivadButton;
        private System.Windows.Forms.GroupBox aspectGroupBox;
        private System.Windows.Forms.TableLayoutPanel aspectTableLayoutPanel;
        private System.Windows.Forms.NumericUpDown aspectValueUpDown;
        private System.Windows.Forms.Label aspectLabel;
        private System.Windows.Forms.Label aspectValueLabel;
        private System.Windows.Forms.ComboBox aspectComboBox;
        private System.Windows.Forms.Button aspectButton;
        private System.Windows.Forms.CheckBox autoApplyCheckBox;
        private System.Windows.Forms.GroupBox currentEepromGroupBox;
        private System.Windows.Forms.TextBox currentEepromTextBox;
        private System.Windows.Forms.GroupBox newEepromGroupBox;
        private System.Windows.Forms.TextBox newEepromTextBox;
        private System.Windows.Forms.Button writeEepromButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox controlPortGroupBox;
        private System.Windows.Forms.ComboBox controlPortComboBox;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button readEepromButton;
        private System.Windows.Forms.GroupBox eepromEditorGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown eepromValueUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown eepromOffsetUpDown;
        private System.Windows.Forms.Button defaultsButton;
        private System.Windows.Forms.CheckBox autoChecksumCheckBox;
        private System.Windows.Forms.Button verifyChecksumButton;
    }
}

