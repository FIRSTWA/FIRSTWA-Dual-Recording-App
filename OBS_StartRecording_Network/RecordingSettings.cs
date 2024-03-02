using System;
using System.Windows.Forms;

namespace FIRSTWA_Recorder
{
    public partial class RecordingSettings : Form
    {
        public string Year { get; private set; } = "2024";
        public string IPAddressPROGRAM { get; private set; } = @"192.168.100.35";
        public string IPAddressWIDE { get; private set; } = @"192.168.100.34";
        public string IPAddressPC { get; private set; } = @"192.168.100.70";
        public string BaseDir { get; private set; } = @"c:\\FIRSTWARecorder";

        public RecordingSettings(string year, string pc, string program, string wide, string basedir)
        {
            InitializeComponent();

            Year = year;
            IPAddressPC = pc;
            IPAddressPROGRAM = program;
            IPAddressWIDE = wide;
            BaseDir = basedir;

            numYear.Value = Convert.ToInt16(year);
            txtIPAddressPC.Text = pc;
            txtIPAddressPROGRAM.Text = program;
            txtIPAddressWIDE.Text = wide;
            txtBaseDir.Text = basedir;
           
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            Year = numYear.Value.ToString();
            IPAddressPROGRAM = txtIPAddressPROGRAM.Text;
            IPAddressWIDE = txtIPAddressWIDE.Text;
            IPAddressPC = txtIPAddressPC.Text;
            BaseDir = txtBaseDir.Text;

            
       
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}
