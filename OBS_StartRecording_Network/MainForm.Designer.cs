﻿namespace FIRSTWA_Recorder
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
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.btnStopRecording = new System.Windows.Forms.Button();
            this.radioBtnQual = new System.Windows.Forms.RadioButton();
            this.radioBtnQuarter = new System.Windows.Forms.RadioButton();
            this.radioBtnSemi = new System.Windows.Forms.RadioButton();
            this.radioBtnFinal = new System.Windows.Forms.RadioButton();
            this.groupMatchTypes = new System.Windows.Forms.GroupBox();
            this.radioBtnPractice = new System.Windows.Forms.RadioButton();
            this.numMatchNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkReplay = new System.Windows.Forms.CheckBox();
            this.numReplayNumber = new System.Windows.Forms.NumericUpDown();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupEvent = new System.Windows.Forms.GroupBox();
            this.comboEventName = new System.Windows.Forms.ComboBox();
            this.chkRecordWide = new System.Windows.Forms.CheckBox();
            this.chkProgramRecord = new System.Windows.Forms.CheckBox();
            this.groupMatch = new System.Windows.Forms.GroupBox();
            this.numFinalNo = new System.Windows.Forms.NumericUpDown();
            this.lblFinalNo = new System.Windows.Forms.Label();
            this.timerElapsed = new System.Windows.Forms.Timer(this.components);
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ledRecordWIDE = new System.Windows.Forms.Panel();
            this.ledRecordPROGRAM = new System.Windows.Forms.Panel();
            this.btnOpenRecordings = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupOBS = new System.Windows.Forms.GroupBox();
            this.btnConnectWide = new System.Windows.Forms.Button();
            this.btnConnectProgram = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblProgramPath = new System.Windows.Forms.LinkLabel();
            this.lblWidePath = new System.Windows.Forms.LinkLabel();
            this.groupMatchDetails = new System.Windows.Forms.GroupBox();
            this.lblMatchNumber = new System.Windows.Forms.Label();
            this.groupBlueAlliance = new System.Windows.Forms.GroupBox();
            this.lblBlue3 = new System.Windows.Forms.Label();
            this.lblBlue1 = new System.Windows.Forms.Label();
            this.lblBlue2 = new System.Windows.Forms.Label();
            this.groupRedAlliance = new System.Windows.Forms.GroupBox();
            this.lblRed3 = new System.Windows.Forms.Label();
            this.lblRed2 = new System.Windows.Forms.Label();
            this.lblRed1 = new System.Windows.Forms.Label();
            this.btnGetMatchDetails = new System.Windows.Forms.Button();
            this.timerTCPWatchDog = new System.Windows.Forms.Timer(this.components);
            this.bgWorker_FTP = new System.ComponentModel.BackgroundWorker();
            this.groupMatchTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMatchNumber)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReplayNumber)).BeginInit();
            this.groupEvent.SuspendLayout();
            this.groupMatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalNo)).BeginInit();
            this.groupStatus.SuspendLayout();
            this.groupOBS.SuspendLayout();
            this.groupMatchDetails.SuspendLayout();
            this.groupBlueAlliance.SuspendLayout();
            this.groupRedAlliance.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.Location = new System.Drawing.Point(22, 526);
            this.btnStartRecording.Margin = new System.Windows.Forms.Padding(6);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.Size = new System.Drawing.Size(334, 96);
            this.btnStartRecording.TabIndex = 0;
            this.btnStartRecording.Text = "Start Recording";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // btnStopRecording
            // 
            this.btnStopRecording.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStopRecording.Enabled = false;
            this.btnStopRecording.Location = new System.Drawing.Point(405, 526);
            this.btnStopRecording.Margin = new System.Windows.Forms.Padding(6);
            this.btnStopRecording.Name = "btnStopRecording";
            this.btnStopRecording.Size = new System.Drawing.Size(334, 96);
            this.btnStopRecording.TabIndex = 0;
            this.btnStopRecording.Text = "Stop Recording";
            this.btnStopRecording.UseVisualStyleBackColor = true;
            this.btnStopRecording.Click += new System.EventHandler(this.btnStopRecording_Click);
            // 
            // radioBtnQual
            // 
            this.radioBtnQual.AutoSize = true;
            this.radioBtnQual.Location = new System.Drawing.Point(139, 35);
            this.radioBtnQual.Margin = new System.Windows.Forms.Padding(6);
            this.radioBtnQual.Name = "radioBtnQual";
            this.radioBtnQual.Size = new System.Drawing.Size(144, 29);
            this.radioBtnQual.TabIndex = 2;
            this.radioBtnQual.Text = "Qualification";
            this.radioBtnQual.UseVisualStyleBackColor = true;
            this.radioBtnQual.CheckedChanged += new System.EventHandler(this.radioBtnMatchType_CheckedChanged);
            // 
            // radioBtnQuarter
            // 
            this.radioBtnQuarter.AutoSize = true;
            this.radioBtnQuarter.Location = new System.Drawing.Point(303, 35);
            this.radioBtnQuarter.Margin = new System.Windows.Forms.Padding(6);
            this.radioBtnQuarter.Name = "radioBtnQuarter";
            this.radioBtnQuarter.Size = new System.Drawing.Size(138, 29);
            this.radioBtnQuarter.TabIndex = 2;
            this.radioBtnQuarter.Text = "Quarterfinal";
            this.radioBtnQuarter.UseVisualStyleBackColor = true;
            this.radioBtnQuarter.CheckedChanged += new System.EventHandler(this.radioBtnMatchType_CheckedChanged);
            // 
            // radioBtnSemi
            // 
            this.radioBtnSemi.AutoSize = true;
            this.radioBtnSemi.Location = new System.Drawing.Point(458, 35);
            this.radioBtnSemi.Margin = new System.Windows.Forms.Padding(6);
            this.radioBtnSemi.Name = "radioBtnSemi";
            this.radioBtnSemi.Size = new System.Drawing.Size(117, 29);
            this.radioBtnSemi.TabIndex = 2;
            this.radioBtnSemi.Text = "Semifinal";
            this.radioBtnSemi.UseVisualStyleBackColor = true;
            this.radioBtnSemi.CheckedChanged += new System.EventHandler(this.radioBtnMatchType_CheckedChanged);
            // 
            // radioBtnFinal
            // 
            this.radioBtnFinal.AutoSize = true;
            this.radioBtnFinal.Location = new System.Drawing.Point(592, 35);
            this.radioBtnFinal.Margin = new System.Windows.Forms.Padding(6);
            this.radioBtnFinal.Name = "radioBtnFinal";
            this.radioBtnFinal.Size = new System.Drawing.Size(79, 29);
            this.radioBtnFinal.TabIndex = 2;
            this.radioBtnFinal.Text = "Final";
            this.radioBtnFinal.UseVisualStyleBackColor = true;
            this.radioBtnFinal.CheckedChanged += new System.EventHandler(this.radioBtnMatchType_CheckedChanged);
            // 
            // groupMatchTypes
            // 
            this.groupMatchTypes.Controls.Add(this.radioBtnPractice);
            this.groupMatchTypes.Controls.Add(this.radioBtnQual);
            this.groupMatchTypes.Controls.Add(this.radioBtnFinal);
            this.groupMatchTypes.Controls.Add(this.radioBtnSemi);
            this.groupMatchTypes.Controls.Add(this.radioBtnQuarter);
            this.groupMatchTypes.Location = new System.Drawing.Point(11, 35);
            this.groupMatchTypes.Margin = new System.Windows.Forms.Padding(6);
            this.groupMatchTypes.Name = "groupMatchTypes";
            this.groupMatchTypes.Padding = new System.Windows.Forms.Padding(6);
            this.groupMatchTypes.Size = new System.Drawing.Size(693, 83);
            this.groupMatchTypes.TabIndex = 3;
            this.groupMatchTypes.TabStop = false;
            this.groupMatchTypes.Text = "Match Type";
            // 
            // radioBtnPractice
            // 
            this.radioBtnPractice.AutoSize = true;
            this.radioBtnPractice.Checked = true;
            this.radioBtnPractice.Location = new System.Drawing.Point(11, 35);
            this.radioBtnPractice.Margin = new System.Windows.Forms.Padding(6);
            this.radioBtnPractice.Name = "radioBtnPractice";
            this.radioBtnPractice.Size = new System.Drawing.Size(107, 29);
            this.radioBtnPractice.TabIndex = 2;
            this.radioBtnPractice.TabStop = true;
            this.radioBtnPractice.Text = "Practice";
            this.radioBtnPractice.UseVisualStyleBackColor = true;
            this.radioBtnPractice.CheckedChanged += new System.EventHandler(this.radioBtnMatchType_CheckedChanged);
            // 
            // numMatchNumber
            // 
            this.numMatchNumber.Location = new System.Drawing.Point(127, 120);
            this.numMatchNumber.Margin = new System.Windows.Forms.Padding(6);
            this.numMatchNumber.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMatchNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMatchNumber.Name = "numMatchNumber";
            this.numMatchNumber.Size = new System.Drawing.Size(95, 29);
            this.numMatchNumber.TabIndex = 6;
            this.numMatchNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Match No:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1225, 42);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "Menu Strip";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordingToolStripMenuItem,
            this.uploadsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(99, 34);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // recordingToolStripMenuItem
            // 
            this.recordingToolStripMenuItem.Name = "recordingToolStripMenuItem";
            this.recordingToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.recordingToolStripMenuItem.Text = "Recording";
            this.recordingToolStripMenuItem.Click += new System.EventHandler(this.recordingToolStripMenuItem_Click);
            // 
            // uploadsToolStripMenuItem
            // 
            this.uploadsToolStripMenuItem.Name = "uploadsToolStripMenuItem";
            this.uploadsToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.uploadsToolStripMenuItem.Text = "Uploading";
            this.uploadsToolStripMenuItem.Click += new System.EventHandler(this.uploadsToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Event Name:";
            // 
            // chkReplay
            // 
            this.chkReplay.AutoSize = true;
            this.chkReplay.Location = new System.Drawing.Point(469, 122);
            this.chkReplay.Margin = new System.Windows.Forms.Padding(6);
            this.chkReplay.Name = "chkReplay";
            this.chkReplay.Size = new System.Drawing.Size(98, 29);
            this.chkReplay.TabIndex = 11;
            this.chkReplay.Text = "Replay";
            this.chkReplay.UseVisualStyleBackColor = true;
            this.chkReplay.CheckedChanged += new System.EventHandler(this.chkReplay_CheckedChanged);
            // 
            // numReplayNumber
            // 
            this.numReplayNumber.Location = new System.Drawing.Point(589, 120);
            this.numReplayNumber.Margin = new System.Windows.Forms.Padding(6);
            this.numReplayNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numReplayNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReplayNumber.Name = "numReplayNumber";
            this.numReplayNumber.Size = new System.Drawing.Size(95, 29);
            this.numReplayNumber.TabIndex = 6;
            this.numReplayNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReplayNumber.ValueChanged += new System.EventHandler(this.numReplayNumber_ValueChanged);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Storage location for recordings";
            // 
            // groupEvent
            // 
            this.groupEvent.Controls.Add(this.comboEventName);
            this.groupEvent.Controls.Add(this.chkRecordWide);
            this.groupEvent.Controls.Add(this.chkProgramRecord);
            this.groupEvent.Controls.Add(this.label3);
            this.groupEvent.Location = new System.Drawing.Point(22, 185);
            this.groupEvent.Margin = new System.Windows.Forms.Padding(6);
            this.groupEvent.Name = "groupEvent";
            this.groupEvent.Padding = new System.Windows.Forms.Padding(6);
            this.groupEvent.Size = new System.Drawing.Size(717, 142);
            this.groupEvent.TabIndex = 12;
            this.groupEvent.TabStop = false;
            this.groupEvent.Text = "Event Specific";
            // 
            // comboEventName
            // 
            this.comboEventName.FormattingEnabled = true;
            this.comboEventName.Location = new System.Drawing.Point(156, 46);
            this.comboEventName.Margin = new System.Windows.Forms.Padding(6);
            this.comboEventName.Name = "comboEventName";
            this.comboEventName.Size = new System.Drawing.Size(450, 32);
            this.comboEventName.TabIndex = 12;
            this.comboEventName.SelectedIndexChanged += new System.EventHandler(this.comboEventName_SelectedIndexChanged);
            // 
            // chkRecordWide
            // 
            this.chkRecordWide.AutoSize = true;
            this.chkRecordWide.Checked = true;
            this.chkRecordWide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecordWide.Location = new System.Drawing.Point(216, 100);
            this.chkRecordWide.Margin = new System.Windows.Forms.Padding(6);
            this.chkRecordWide.Name = "chkRecordWide";
            this.chkRecordWide.Size = new System.Drawing.Size(151, 29);
            this.chkRecordWide.TabIndex = 11;
            this.chkRecordWide.Text = "Record Wide";
            this.chkRecordWide.UseVisualStyleBackColor = true;
            // 
            // chkProgramRecord
            // 
            this.chkProgramRecord.AutoSize = true;
            this.chkProgramRecord.Checked = true;
            this.chkProgramRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProgramRecord.Location = new System.Drawing.Point(17, 100);
            this.chkProgramRecord.Margin = new System.Windows.Forms.Padding(6);
            this.chkProgramRecord.Name = "chkProgramRecord";
            this.chkProgramRecord.Size = new System.Drawing.Size(179, 29);
            this.chkProgramRecord.TabIndex = 11;
            this.chkProgramRecord.Text = "Record Program";
            this.chkProgramRecord.UseVisualStyleBackColor = true;
            // 
            // groupMatch
            // 
            this.groupMatch.Controls.Add(this.groupMatchTypes);
            this.groupMatch.Controls.Add(this.numFinalNo);
            this.groupMatch.Controls.Add(this.numMatchNumber);
            this.groupMatch.Controls.Add(this.chkReplay);
            this.groupMatch.Controls.Add(this.numReplayNumber);
            this.groupMatch.Controls.Add(this.lblFinalNo);
            this.groupMatch.Controls.Add(this.label2);
            this.groupMatch.Location = new System.Drawing.Point(22, 338);
            this.groupMatch.Margin = new System.Windows.Forms.Padding(6);
            this.groupMatch.Name = "groupMatch";
            this.groupMatch.Padding = new System.Windows.Forms.Padding(6);
            this.groupMatch.Size = new System.Drawing.Size(717, 177);
            this.groupMatch.TabIndex = 13;
            this.groupMatch.TabStop = false;
            this.groupMatch.Text = "Match Specific";
            // 
            // numFinalNo
            // 
            this.numFinalNo.Location = new System.Drawing.Point(343, 120);
            this.numFinalNo.Margin = new System.Windows.Forms.Padding(6);
            this.numFinalNo.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numFinalNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFinalNo.Name = "numFinalNo";
            this.numFinalNo.Size = new System.Drawing.Size(95, 29);
            this.numFinalNo.TabIndex = 6;
            this.numFinalNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFinalNo.Visible = false;
            // 
            // lblFinalNo
            // 
            this.lblFinalNo.AutoSize = true;
            this.lblFinalNo.Location = new System.Drawing.Point(242, 124);
            this.lblFinalNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFinalNo.Name = "lblFinalNo";
            this.lblFinalNo.Size = new System.Drawing.Size(78, 25);
            this.lblFinalNo.TabIndex = 7;
            this.lblFinalNo.Text = "Set No:";
            this.lblFinalNo.Visible = false;
            // 
            // timerElapsed
            // 
            this.timerElapsed.Interval = 10;
            this.timerElapsed.Tick += new System.EventHandler(this.timerElapsed_Tick);
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.Location = new System.Drawing.Point(257, 631);
            this.lblElapsedTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(240, 48);
            this.lblElapsedTime.TabIndex = 14;
            this.lblElapsedTime.Text = "00:00:00.00";
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.label5);
            this.groupStatus.Controls.Add(this.label4);
            this.groupStatus.Controls.Add(this.ledRecordWIDE);
            this.groupStatus.Controls.Add(this.ledRecordPROGRAM);
            this.groupStatus.Location = new System.Drawing.Point(750, 185);
            this.groupStatus.Margin = new System.Windows.Forms.Padding(6);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Padding = new System.Windows.Forms.Padding(6);
            this.groupStatus.Size = new System.Drawing.Size(290, 142);
            this.groupStatus.TabIndex = 17;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Wide Recording:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Program Recording:";
            // 
            // ledRecordWIDE
            // 
            this.ledRecordWIDE.BackColor = System.Drawing.Color.Gray;
            this.ledRecordWIDE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledRecordWIDE.Location = new System.Drawing.Point(207, 100);
            this.ledRecordWIDE.Margin = new System.Windows.Forms.Padding(6);
            this.ledRecordWIDE.Name = "ledRecordWIDE";
            this.ledRecordWIDE.Size = new System.Drawing.Size(57, 22);
            this.ledRecordWIDE.TabIndex = 0;
            // 
            // ledRecordPROGRAM
            // 
            this.ledRecordPROGRAM.BackColor = System.Drawing.Color.Gray;
            this.ledRecordPROGRAM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledRecordPROGRAM.Location = new System.Drawing.Point(207, 52);
            this.ledRecordPROGRAM.Margin = new System.Windows.Forms.Padding(6);
            this.ledRecordPROGRAM.Name = "ledRecordPROGRAM";
            this.ledRecordPROGRAM.Size = new System.Drawing.Size(57, 22);
            this.ledRecordPROGRAM.TabIndex = 0;
            // 
            // btnOpenRecordings
            // 
            this.btnOpenRecordings.Location = new System.Drawing.Point(22, 720);
            this.btnOpenRecordings.Margin = new System.Windows.Forms.Padding(6);
            this.btnOpenRecordings.Name = "btnOpenRecordings";
            this.btnOpenRecordings.Size = new System.Drawing.Size(334, 42);
            this.btnOpenRecordings.TabIndex = 18;
            this.btnOpenRecordings.Text = "Open Recordings Folder";
            this.btnOpenRecordings.UseVisualStyleBackColor = true;
            this.btnOpenRecordings.Click += new System.EventHandler(this.btnOpenRecordings_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(750, 338);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(6);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(290, 42);
            this.btnUpload.TabIndex = 19;
            this.btnUpload.Text = "Upload to Youtube";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // groupOBS
            // 
            this.groupOBS.Controls.Add(this.btnConnectWide);
            this.groupOBS.Controls.Add(this.btnConnectProgram);
            this.groupOBS.Location = new System.Drawing.Point(22, 74);
            this.groupOBS.Margin = new System.Windows.Forms.Padding(6);
            this.groupOBS.Name = "groupOBS";
            this.groupOBS.Padding = new System.Windows.Forms.Padding(6);
            this.groupOBS.Size = new System.Drawing.Size(717, 100);
            this.groupOBS.TabIndex = 22;
            this.groupOBS.TabStop = false;
            this.groupOBS.Text = "DeckLink Connections";
            // 
            // btnConnectWide
            // 
            this.btnConnectWide.Location = new System.Drawing.Point(383, 35);
            this.btnConnectWide.Margin = new System.Windows.Forms.Padding(6);
            this.btnConnectWide.Name = "btnConnectWide";
            this.btnConnectWide.Size = new System.Drawing.Size(323, 42);
            this.btnConnectWide.TabIndex = 19;
            this.btnConnectWide.Text = "Connect to Wide Decklink";
            this.btnConnectWide.UseVisualStyleBackColor = true;
            this.btnConnectWide.Click += new System.EventHandler(this.btnConnectWide_Click);
            // 
            // btnConnectProgram
            // 
            this.btnConnectProgram.Location = new System.Drawing.Point(11, 35);
            this.btnConnectProgram.Margin = new System.Windows.Forms.Padding(6);
            this.btnConnectProgram.Name = "btnConnectProgram";
            this.btnConnectProgram.Size = new System.Drawing.Size(323, 42);
            this.btnConnectProgram.TabIndex = 19;
            this.btnConnectProgram.Text = "Connect to Program Decklink";
            this.btnConnectProgram.UseVisualStyleBackColor = true;
            this.btnConnectProgram.Click += new System.EventHandler(this.btnConnectProgram_Click);
            // 
            // lblProgramPath
            // 
            this.lblProgramPath.Location = new System.Drawing.Point(0, 0);
            this.lblProgramPath.Name = "lblProgramPath";
            this.lblProgramPath.Size = new System.Drawing.Size(100, 23);
            this.lblProgramPath.TabIndex = 28;
            // 
            // lblWidePath
            // 
            this.lblWidePath.Location = new System.Drawing.Point(0, 0);
            this.lblWidePath.Name = "lblWidePath";
            this.lblWidePath.Size = new System.Drawing.Size(100, 23);
            this.lblWidePath.TabIndex = 27;
            // 
            // groupMatchDetails
            // 
            this.groupMatchDetails.Controls.Add(this.lblMatchNumber);
            this.groupMatchDetails.Controls.Add(this.groupBlueAlliance);
            this.groupMatchDetails.Controls.Add(this.groupRedAlliance);
            this.groupMatchDetails.Location = new System.Drawing.Point(750, 463);
            this.groupMatchDetails.Margin = new System.Windows.Forms.Padding(4);
            this.groupMatchDetails.Name = "groupMatchDetails";
            this.groupMatchDetails.Padding = new System.Windows.Forms.Padding(4);
            this.groupMatchDetails.Size = new System.Drawing.Size(431, 238);
            this.groupMatchDetails.TabIndex = 25;
            this.groupMatchDetails.TabStop = false;
            this.groupMatchDetails.Text = "Match Details";
            // 
            // lblMatchNumber
            // 
            this.lblMatchNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMatchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchNumber.Location = new System.Drawing.Point(95, 28);
            this.lblMatchNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMatchNumber.Name = "lblMatchNumber";
            this.lblMatchNumber.Size = new System.Drawing.Size(229, 52);
            this.lblMatchNumber.TabIndex = 2;
            this.lblMatchNumber.Text = "MATCH ##";
            this.lblMatchNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBlueAlliance
            // 
            this.groupBlueAlliance.Controls.Add(this.lblBlue3);
            this.groupBlueAlliance.Controls.Add(this.lblBlue1);
            this.groupBlueAlliance.Controls.Add(this.lblBlue2);
            this.groupBlueAlliance.ForeColor = System.Drawing.Color.Blue;
            this.groupBlueAlliance.Location = new System.Drawing.Point(215, 83);
            this.groupBlueAlliance.Margin = new System.Windows.Forms.Padding(4);
            this.groupBlueAlliance.Name = "groupBlueAlliance";
            this.groupBlueAlliance.Padding = new System.Windows.Forms.Padding(4);
            this.groupBlueAlliance.Size = new System.Drawing.Size(200, 137);
            this.groupBlueAlliance.TabIndex = 1;
            this.groupBlueAlliance.TabStop = false;
            this.groupBlueAlliance.Text = "BLUE ALLIANCE";
            // 
            // lblBlue3
            // 
            this.lblBlue3.Location = new System.Drawing.Point(7, 94);
            this.lblBlue3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlue3.Name = "lblBlue3";
            this.lblBlue3.Size = new System.Drawing.Size(185, 20);
            this.lblBlue3.TabIndex = 2;
            this.lblBlue3.Text = "BLUE3";
            this.lblBlue3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBlue1
            // 
            this.lblBlue1.Location = new System.Drawing.Point(7, 28);
            this.lblBlue1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlue1.Name = "lblBlue1";
            this.lblBlue1.Size = new System.Drawing.Size(185, 20);
            this.lblBlue1.TabIndex = 0;
            this.lblBlue1.Text = "BLUE1";
            this.lblBlue1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBlue2
            // 
            this.lblBlue2.Location = new System.Drawing.Point(7, 61);
            this.lblBlue2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBlue2.Name = "lblBlue2";
            this.lblBlue2.Size = new System.Drawing.Size(185, 20);
            this.lblBlue2.TabIndex = 1;
            this.lblBlue2.Text = "BLUE2";
            this.lblBlue2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupRedAlliance
            // 
            this.groupRedAlliance.Controls.Add(this.lblRed3);
            this.groupRedAlliance.Controls.Add(this.lblRed2);
            this.groupRedAlliance.Controls.Add(this.lblRed1);
            this.groupRedAlliance.ForeColor = System.Drawing.Color.Red;
            this.groupRedAlliance.Location = new System.Drawing.Point(7, 83);
            this.groupRedAlliance.Margin = new System.Windows.Forms.Padding(4);
            this.groupRedAlliance.Name = "groupRedAlliance";
            this.groupRedAlliance.Padding = new System.Windows.Forms.Padding(4);
            this.groupRedAlliance.Size = new System.Drawing.Size(200, 137);
            this.groupRedAlliance.TabIndex = 0;
            this.groupRedAlliance.TabStop = false;
            this.groupRedAlliance.Text = "RED ALLIANCE";
            // 
            // lblRed3
            // 
            this.lblRed3.Location = new System.Drawing.Point(7, 94);
            this.lblRed3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRed3.Name = "lblRed3";
            this.lblRed3.Size = new System.Drawing.Size(185, 20);
            this.lblRed3.TabIndex = 2;
            this.lblRed3.Text = "RED3";
            this.lblRed3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRed2
            // 
            this.lblRed2.Location = new System.Drawing.Point(7, 61);
            this.lblRed2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRed2.Name = "lblRed2";
            this.lblRed2.Size = new System.Drawing.Size(185, 20);
            this.lblRed2.TabIndex = 1;
            this.lblRed2.Text = "RED2";
            this.lblRed2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRed1
            // 
            this.lblRed1.Location = new System.Drawing.Point(7, 28);
            this.lblRed1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRed1.Name = "lblRed1";
            this.lblRed1.Size = new System.Drawing.Size(185, 20);
            this.lblRed1.TabIndex = 0;
            this.lblRed1.Text = "RED1";
            this.lblRed1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetMatchDetails
            // 
            this.btnGetMatchDetails.Location = new System.Drawing.Point(812, 414);
            this.btnGetMatchDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetMatchDetails.Name = "btnGetMatchDetails";
            this.btnGetMatchDetails.Size = new System.Drawing.Size(290, 42);
            this.btnGetMatchDetails.TabIndex = 26;
            this.btnGetMatchDetails.Text = "Get Match Details";
            this.btnGetMatchDetails.UseVisualStyleBackColor = true;
            this.btnGetMatchDetails.Click += new System.EventHandler(this.btnGetMatchDetails_Click);
            // 
            // timerTCPWatchDog
            // 
            this.timerTCPWatchDog.Interval = 1000;
            this.timerTCPWatchDog.Tick += new System.EventHandler(this.timerTCPWatchDog_Tick);
            // 
            // bgWorker_FTP
            // 
            this.bgWorker_FTP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_FTP_DoWork);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnStartRecording;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnStopRecording;
            this.ClientSize = new System.Drawing.Size(1225, 892);
            this.Controls.Add(this.btnGetMatchDetails);
            this.Controls.Add(this.groupMatchDetails);
            this.Controls.Add(this.lblWidePath);
            this.Controls.Add(this.lblProgramPath);
            this.Controls.Add(this.groupOBS);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnOpenRecordings);
            this.Controls.Add(this.groupStatus);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.groupMatch);
            this.Controls.Add(this.groupEvent);
            this.Controls.Add(this.btnStopRecording);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIRSTWA Recording";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.groupMatchTypes.ResumeLayout(false);
            this.groupMatchTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMatchNumber)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReplayNumber)).EndInit();
            this.groupEvent.ResumeLayout(false);
            this.groupEvent.PerformLayout();
            this.groupMatch.ResumeLayout(false);
            this.groupMatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalNo)).EndInit();
            this.groupStatus.ResumeLayout(false);
            this.groupStatus.PerformLayout();
            this.groupOBS.ResumeLayout(false);
            this.groupMatchDetails.ResumeLayout(false);
            this.groupBlueAlliance.ResumeLayout(false);
            this.groupRedAlliance.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartRecording;
        private System.Windows.Forms.Button btnStopRecording;
        private System.Windows.Forms.RadioButton radioBtnQual;
        private System.Windows.Forms.RadioButton radioBtnQuarter;
        private System.Windows.Forms.RadioButton radioBtnSemi;
        private System.Windows.Forms.RadioButton radioBtnFinal;
        private System.Windows.Forms.GroupBox groupMatchTypes;
        private System.Windows.Forms.NumericUpDown numMatchNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioBtnPractice;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chkReplay;
        private System.Windows.Forms.NumericUpDown numReplayNumber;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupEvent;
        private System.Windows.Forms.GroupBox groupMatch;
        private System.Windows.Forms.Timer timerElapsed;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.GroupBox groupStatus;
        private System.Windows.Forms.Panel ledRecordPROGRAM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel ledRecordWIDE;
        private System.Windows.Forms.CheckBox chkRecordWide;
        private System.Windows.Forms.CheckBox chkProgramRecord;
        private System.Windows.Forms.Button btnOpenRecordings;
        private System.Windows.Forms.ToolStripMenuItem recordingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadsToolStripMenuItem;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ComboBox comboEventName;
        private System.Windows.Forms.GroupBox groupOBS;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel lblProgramPath;
        private System.Windows.Forms.LinkLabel lblWidePath;
        private System.Windows.Forms.GroupBox groupMatchDetails;
        private System.Windows.Forms.Label lblMatchNumber;
        private System.Windows.Forms.GroupBox groupBlueAlliance;
        private System.Windows.Forms.GroupBox groupRedAlliance;
        private System.Windows.Forms.Button btnGetMatchDetails;
        private System.Windows.Forms.Label lblBlue3;
        private System.Windows.Forms.Label lblBlue1;
        private System.Windows.Forms.Label lblBlue2;
        private System.Windows.Forms.Label lblRed3;
        private System.Windows.Forms.Label lblRed2;
        private System.Windows.Forms.Label lblRed1;
        private System.Windows.Forms.NumericUpDown numFinalNo;
        private System.Windows.Forms.Label lblFinalNo;
        private System.Windows.Forms.Timer timerTCPWatchDog;
        private System.Windows.Forms.Button btnConnectWide;
        private System.Windows.Forms.Button btnConnectProgram;
        private System.ComponentModel.BackgroundWorker bgWorker_FTP;
    }
}

