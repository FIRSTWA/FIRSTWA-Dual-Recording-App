﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Win32;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using NLog;
using Microsoft.VisualBasic;

/* TODO:
 * Error handling for videos that failed to record, transfer, or convert
 * Figure out a good versioning scheme
 *      Start with "year.version"
 * Figure out how to record long-run ceremonies (i.e. Opening/Closing ceremonies and awards).  
 *      I want to be able to record up to 1.5 hours to be safe.
 * If the match isn't found when the "start recording" button is pressed, don't halt the recording.
 *      The user might have forgotton to switch off of quarterfinals and needs to start recording.
 *      Give the user the option to change the match type and number is it can't be found in TBA.
 * 
 * Error handling
 * Commenting
 * Layout/UI Design
 */

using FileName = System.String;
using FilePath = System.String;
using URI = System.String;
using IPAddress = System.String;
using TCPPort = System.String;
using RegistryKeyName = System.String;

namespace FIRSTWA_Recorder
{

    public enum MapMono
    {
        None,
        Left,
        Right
    };

    public enum FormState
    {
        Idle,
        Recording,
        Processing
    };

    public partial class MainForm : Form
    {
        private const string BASESUBKEY = @"Software\FIRSTWA";

        RecordingSettings frmRecordingSetting;
        AudioSettings frmAudioSetting;
        
        RestClient tbaClient = new RestClient("http://www.thebluealliance.com/api/v3");
        RestRequest tbaRequest;
        private string TBAKEY = "9FjTZaWXf1rKVnPneSZlRUGSN5vq9VAH467lSZpxEZ69OtHy4YvvKB9qWbzueSu9";

        List<District> eventDistrict = new List<District>();
        List<Event> eventDetails = new List<Event>();
        Event currentEvent = new Event();
        Match currentMatch;
        string matchType;

        Match[] matches;

        string strYear = "2024";
        IPAddress strIPAddressPC = @"192.168.100.70";
        IPAddress strIPAddressPROGRAM = @"192.168.100.35";
        IPAddress strIPAddressWIDE = @"192.168.100.34";
        TCPPort strPortPROGRAM = "9993";
        TCPPort strPortWIDE = "9993";
        string strBaseDir = "c:\\FIRSTWARecorder";

        MapMono progChannels = MapMono.None;
        MapMono wideChannels = MapMono.None;

        FormState state;

        RegistryKeyName regYear = "YEAR";
        RegistryKeyName regPROGRAM = "PROGRAM_IPAddress";
        RegistryKeyName regWIDE = "WIDE_IPAddress";
        RegistryKeyName regPC = "PC_IPAddress";
        RegistryKeyName regProgAudio = "PROGRAM_AudioChannel";
        RegistryKeyName regWideAudio = "WIDE_AudioChannel";
        RegistryKeyName regAPIKey = "apikey";
        // 09659FjTZaWXf1rKVnPneSZlRUGSN5vq9VAH467lSZpxEZ69OtHy4YvvKB9qWbzueSu9
        RegistryKeyName regBaseDir = "basedir";
        List<RegistryKeyName> registryKeyNames = new List<FileName>();
        int progress=0;
        HyperDeck hdProgram, hdWide;

        string matchNameProgram = "";
        string matchNameWide = "";
        // string matchABV = "";
        string matchABVWide = "";
        string matchABVProgram = "";

        long tickStamp = 0;  // Used as a serial number for recording files

        FileName fileNameProgram, fileNameWide;
        private FilePath tempFolder;
        private string ytDescription, ytTags;

        private DateTime startTime;

        private string programVideoTitle, wideVideoTitle;

//        private bool wideFTPUploadFail = false;
//        private bool programFTPUploadFail = false;

        private bool bYoutubePopup = false;

        private bool PCPingable = false;
        private Ping pinger = null;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        enum MatchType
        {
            Qualification,
            Quarterfinal,
            Semifinal,
            Final,
            Ceremony
        }
        MatchType currentMatchType = MatchType.Qualification;

        public bool UpdateTBA()
        {
            logger.Info("... Reading TBA API key from registry");
            string strReq = "/district/" + strYear + "pnw/events";
            tbaRequest = new RestRequest(strReq, Method.GET);
            TBAKEY = ReadRegistryKey(regAPIKey);

            if (TBAKEY == null)
            {
                logger.Fatal("- Failed: Could not find TBA API key");
                TBAKEY = Interaction.InputBox("Please enter the TBA API key.", "TBA API Key", "");

                if (TBAKEY == "")
                {
                    MessageBox.Show("Could not find a TBA API key in the registry.  Closing...");
                    return false;
                }

                WriteRegistryKey(regAPIKey, TBAKEY);
            }

            logger.Info("... Getting event list from TBA");
            tbaRequest.AddHeader
            (
                "X-TBA-Auth-Key",
                TBAKEY
            );

            string tbaContent;


            IRestResponse tbaResponse = tbaClient.Execute(tbaRequest);
            tbaContent = tbaResponse.Content;
            tbaContent = tbaContent.Trim('"');

            //
            // TBA has in the past sent us incomplete or invalid data 
            // That causes an exception and was crashing this program
            // Do the following in a try catch
            //

            try 
            {
                eventDistrict = JsonConvert.DeserializeObject<List<District>>(tbaContent);
            }
            catch (Exception ex)
            { 
            
               MessageBox.Show("TBA District Data Exception: " + ex.HResult.ToString());

            }
            try
            {
                eventDetails = JsonConvert.DeserializeObject<List<Event>>(tbaContent);
            }
            catch (Exception ex)
            { 

               MessageBox.Show("TBA Event Data Exception" + ex.HResult.ToString());

            }





            

            // clear out anything that is already there

            comboEventName.Items.Clear();

            eventDetails.ForEach(x => comboEventName.Items.Add((x.week + 1) + " - " + x.first_event_code + " - " + x.short_name));
            comboEventName.Sorted = true;
            comboEventName.Items.Add("Custom Event");

            return true; 

        }

        public MainForm()
        {
            InitializeComponent();

            logger.Info("Initializing Main Form");

            state = FormState.Idle;

            registryKeyNames.Add(regPROGRAM);
            registryKeyNames.Add(regWIDE);
            registryKeyNames.Add(regYear);
            registryKeyNames.Add(regPC);
            registryKeyNames.Add(regProgAudio);
            registryKeyNames.Add(regWideAudio);
            registryKeyNames.Add(regBaseDir);

            logger.Info("... Reading form variables from registry");
            try
            {
                //
                // Run through the registry keys we were expecting. If they aren't
                // all there, then do a default registry key creation
                //
                foreach (RegistryKeyName keyName in registryKeyNames)
                {
                    if (ReadRegistryKey(keyName) == null)
                    {
                        UpdateRegistryKeys();
                    }
                }

                //
                // Should be the case we have verified all of the registry keys
                // Now grab the ones we need.
                //

                strYear = ReadRegistryKey(regYear);
                strIPAddressPC = ReadRegistryKey(regPC);
                strIPAddressPROGRAM = ReadRegistryKey(regPROGRAM);
                strIPAddressWIDE = ReadRegistryKey(regWIDE);
                strBaseDir = ReadRegistryKey(regBaseDir);

                Enum.TryParse(ReadRegistryKey(regWideAudio), out MapMono _wideChannels);
                Enum.TryParse(ReadRegistryKey(regProgAudio), out MapMono _progChannels);

                wideChannels = _wideChannels;
                progChannels = _progChannels;
            }
            catch
            {
                logger.Error("... Could not find form registry keys, creating defaults");
                UpdateRegistryKeys();
                MessageBox.Show("Initialized the registry keys.  Please check that the registry keys are correct.");
            }

            // Insure that the base directory structure exists
            CreateBaseDirectoryStructure();

            // Grab TBA info

            UpdateTBA();        

           
            logger.Info("... Initializing Settings Forms");
            frmRecordingSetting = new RecordingSettings(strYear, strIPAddressPC, strIPAddressPROGRAM, strIPAddressWIDE,strBaseDir);
            frmAudioSetting = new AudioSettings(wideChannels,progChannels);

            if (!Debugger.IsAttached)
            {
                groupEvent.Enabled = false;
                groupMatch.Enabled = false;
                btnStartRecording.Enabled = false;
                btnStopRecording.Enabled = false;
                logger.Info("- Done");
            }
        }

        public void CreateBaseDirectoryStructure()
        {
            logger.Info("... Creating Base directory" + strBaseDir);

            try
            {
                if (!Directory.Exists(strBaseDir))
                {
                    Directory.CreateDirectory(strBaseDir);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Creating " + strBaseDir + " failed "+ ex.Message + "\nNeed to change Basedir in Settings->Recording?");
                return;
            }


            tempFolder = strBaseDir + "\\Temp";
            logger.Info("... Creating Temp directory" + tempFolder);
           
            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

        }
        #region Registry
        private string ReadRegistryKey(string key)
        {
            RegistryKey firstwaKey = Registry.CurrentUser.OpenSubKey(BASESUBKEY, true);
            if (firstwaKey == null)
            {
                return null;
            }
            else
            {
                var fwKeyValue = firstwaKey.GetValue(key);

                if(fwKeyValue != null)
                {
                    return fwKeyValue.ToString();
                }

                return null;
            }

        }

        private void UpdateRegistryKeys()
        {
            WriteRegistryKey(regYear, strYear);
            WriteRegistryKey(regPC, strIPAddressPC);
            WriteRegistryKey(regPROGRAM, strIPAddressPROGRAM);
            WriteRegistryKey(regWIDE, strIPAddressWIDE);
            WriteRegistryKey(regWideAudio, wideChannels.ToString());
            WriteRegistryKey(regProgAudio, progChannels.ToString());
            WriteRegistryKey(regBaseDir, strBaseDir);

        }

        private void WriteRegistryKey(string key, string value)
        {
            RegistryKey firstwaKey = Registry.CurrentUser.OpenSubKey(BASESUBKEY, true);
            if (firstwaKey == null)
            {
                firstwaKey = Registry.CurrentUser.CreateSubKey(BASESUBKEY);
            }
            
            firstwaKey.SetValue(key, value);
        }
        #endregion

        private bool SearchValidMatch()
        {
            string matchAbrev = "qm";
            switch (currentMatchType)
            {
                case MatchType.Qualification:
                    matchType = "Q";
                    matchAbrev = "qm";
                    break;
                case MatchType.Quarterfinal:
                    matchType = "QF";
                    matchAbrev = "qf";
                    break;
                case MatchType.Semifinal:
                    matchType = "SF";
                    matchAbrev = "sf";
                    break;
                case MatchType.Final:
                    matchType = "F";
                    matchAbrev = "f";
                    break;
                default:                // This means ceremony
                    matchType = "";
                    matchAbrev = "";
                    break;
            }
            string matchABV; 

            if (currentMatchType == MatchType.Qualification || currentMatchType == MatchType.Final)
            {
                matchABV = string.Format("{0}_{1}{2}v{3}", currentEvent.event_code, matchAbrev, numMatchNumber.Value.ToString(), tickStamp.ToString());
            }
            else if (currentMatchType != MatchType.Ceremony)
            {
                matchABV = string.Format("{0}_{1}{2}m{3}v{4}", currentEvent.event_code, matchAbrev, numFinalNo.Value.ToString(), numMatchNumber.Value.ToString(), tickStamp.ToString());
            }
            else
            {
                matchABV = string.Format("{0}c{1}v{2}", currentEvent.event_code, txtCeremonyTitle.Text.ToString(),tickStamp.ToString());
            }

            currentMatch = null;
            foreach (Match match in matches)
            {
                if (match.CompLevel.Equals(matchAbrev))
                {
                    if (match.MatchNumber == numMatchNumber.Value && match.SetNumber == numFinalNo.Value)
                    {
                        currentMatch = match;
                        break;
                    }
                }
            }
            matchABVWide = matchABV + "_w";
            matchABVProgram = matchABV + "_p";
            if (currentMatch == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            logger.Info("Attempting to start recording");
            if (comboEventName.SelectedItem == null)
            {
                logger.Error("- Failed: no event selected");
                MessageBox.Show("Please choose an event before recording");
                return;
            }

            tickStamp = tickStamp + 1;  // Assign unique value to this recording click. Likely a unique number

            // Check to see if current match number is valid. Otherwise, try an update
            
            if (!SearchValidMatch())
            {
                // Update to current match list from TBA
                GetMatches();

                // Try finding it again
                SearchValidMatch();
                if (!SearchValidMatch())
                {
                    logger.Info("... match not found");
                    var result = MessageBox.Show("Match does not exist!\n\nDo you want to continue recording?", "Error", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        logger.Error("- Failed: User canceled since match was not found");
                        return;
                    }
                }
            }

            logger.Info("... Match abreviation: {0}", matchABVProgram);

            groupEvent.Enabled = false;
            groupMatch.Enabled = false;

            btnStartRecording.Enabled = false;

            Regex sanatize = new Regex("[^a-zA-Z0-9_ ]");
            txtCeremonyTitle.Text = sanatize.Replace(txtCeremonyTitle.Text.ToString(), "");

            if (chkProgramRecord.Checked)
            {
                logger.Info("... Beginning program recording");
                if (currentMatchType == MatchType.Qualification || currentMatchType == MatchType.Final)
                {
                    matchNameProgram = string.Format("{2}{3} {0} {1}", currentEvent.year, currentEvent.name, matchType, numMatchNumber.Value.ToString());
                }
                else if (currentMatchType != MatchType.Ceremony)
                {
                    matchNameProgram = string.Format("{2}{3}-{4} {0} {1}", currentEvent.year, currentEvent.name, matchType, numFinalNo.Value.ToString(), numMatchNumber.Value.ToString());
                }
                else
                {
                    matchNameProgram = string.Format("{0} {1} Ceremony {2}", currentEvent.year, currentEvent.name, txtCeremonyTitle.Text.ToString());
                }

                fileNameProgram = matchNameProgram + tickStamp.ToString() + ".mp4";

                hdProgram.Write("record: name: " + matchABVProgram);
                
                logger.Info("... Program file name: {0}", fileNameProgram);

                string status = hdProgram.Read();
                if (!status.Contains("200"))
                {
                    logger.Error("... Program recording failed to start:");
                    logger.Info(status);
                    btnConnectProgram.BackColor = Color.Yellow;
                }
            }

            if (chkRecordWide.Checked && currentMatchType != MatchType.Ceremony)
            {
                logger.Info("... Beginning wide recording");
                if (currentMatchType == MatchType.Qualification || currentMatchType == MatchType.Final)
                {
                    matchNameWide = string.Format("{2}{3} {0} {1} - WIDE ", currentEvent.year, currentEvent.name, matchType, numMatchNumber.Value.ToString());
                }
                else
                {
                    matchNameWide = string.Format("{2}{3}-{4} {0} {1} - WIDE", currentEvent.year, currentEvent.name, matchType, numFinalNo.Value.ToString(), numMatchNumber.Value.ToString());
                }
                fileNameWide = matchNameWide + tickStamp.ToString() + ".mp4";

                hdWide.Write("record: name: " + matchABVWide);

                
                logger.Info("... Wide file name: {0}", fileNameWide);

                string status = hdWide.Read();
                if (!status.Contains("200"))
                {
                    logger.Error("... Wide recording failed to start:");
                    logger.Info(status);
                    btnConnectWide.BackColor = Color.Yellow;
                }
            }

            startTime = DateTime.Now;
            timerElapsed.Start();

            btnStopRecording.Enabled = true;
            state = FormState.Recording;
            bgWorker_WD.RunWorkerAsync();

            SetProgress(0);
            progress = 0;

            ledProgram.BackColor = Color.Red;
            ledWide.BackColor = Color.Red;
            logger.Info("- Done");
        }

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            logger.Info("Stopping recording");
            btnStopRecording.Enabled = false;
            state = FormState.Processing;

            timerElapsed.Stop();

            if (chkProgramRecord.Checked)
            {
                logger.Info("... Stopping Program Recording");
                hdProgram.Write("stop");
                string status = hdProgram.Read();
                if (!status.Contains("200"))
                {
                    btnConnectProgram.BackColor = Color.Yellow;
                    logger.Error("... Program recording failed to stop:");
                    logger.Info(status);
                }

                bgWorker_FTP_Program.RunWorkerAsync();
            }


            if (currentMatchType != MatchType.Ceremony && chkRecordWide.Checked) {
                logger.Info("... Stopping Wide Recording");
                hdWide.Write("stop");
                string status = hdWide.Read();
                if (!status.Contains("200"))
                {
                    btnConnectWide.BackColor = Color.Yellow;
                    logger.Error("... Wide recording failed to stop:");
                    logger.Info(status);
                }

                bgWorker_FTP_Wide.RunWorkerAsync();
            }

            //
            //  Clear Old files from TEMP folder
            //

            logger.Info("... Clearing old temp files");
            List<string> directories = Directory.GetFiles(tempFolder).ToList();
            List<DateTime> timestamps = new List<DateTime>();

            foreach (string file in directories)
            {
                timestamps.Add(File.GetCreationTime(file));
            }

            if (directories.Count > 10)
            {
                while (directories.Count > 10)
                {
                    int minTimstampIndex = timestamps.IndexOf(timestamps.Min());
                    File.Delete(directories[minTimstampIndex]);

                    timestamps.RemoveAt(minTimstampIndex);
                    directories.RemoveAt(minTimstampIndex);
                }
            }

            logger.Info("... incrementing match numbers");
            if (currentMatchType == MatchType.Qualification || currentMatchType == MatchType.Final)
            {
                if (numMatchNumber.Value < numMatchNumber.Maximum)
                {
                    numMatchNumber.Value++;
                }
            }
            else if(currentMatchType != MatchType.Ceremony)
            {
                if(numFinalNo.Value < numFinalNo.Maximum)
                {
                    numFinalNo.Value++;
                }
                else
                {
                    numFinalNo.Value = 1;
                    if (numMatchNumber.Value < numMatchNumber.Maximum)
                    {
                        numMatchNumber.Value++;
                    }
                }
            }

            // GetMatches();

            btnCancel.Enabled = true;
            groupEvent.Enabled = true;
            groupMatch.Enabled = true;
            btnStartRecording.Enabled = true;
            logger.Info("- Done");
        }

        #region FTP Stuff
        /* private void CreateEventDirectory(URI uriPath)
        {
            logger.Info("Connecting to PC and creating remote directory");
            try
            {
                WebRequest request = WebRequest.Create(uriPath);
                request.Timeout = 2000;
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("FTP_User", "");
                
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    logger.Info("... response:\n{0}", resp.StatusCode);
                }
            }
            catch (WebException e)
            {
                logger.Error("... Failed to connect to PC");
                logger.Info(e.Status);
                logger.Info(e.Message);
                bgWorker_FTP_Program.CancelAsync();
                bgWorker_FTP_Wide.CancelAsync();
                return;
            }
            catch (Exception e)
            {
                logger.Error("... unhandled error");
                logger.Info(e.Message);
            }

        }
        */

        //convert the mp4 from uncompressed audio to mp3 audio using ffmpeg
        //videoPath - filepath of mp4 to convert
       /* private void ConvertVideo(FilePath videoPath, MapMono style)
        {
            logger.Info("Starting Conversion for {0}", videoPath);
            string videoName = videoPath.Substring(0,videoPath.Length - 4);
            string outVideo = videoName + "test.mp4";

            StringBuilder args_proto = new StringBuilder();

            if (style == MapMono.Left)
            {
                args_proto.AppendFormat("-y -acodec pcm_s24le -i \"{0}\" -acodec mp3 -vcodec copy -af \"pan=mono|c0=c0\" \"{1}\"", videoPath, outVideo);
                logger.Info("... Left Mono Pipe for {0}", videoPath);
            }
            else if (style == MapMono.Right)
            {
                args_proto.AppendFormat("-y -acodec pcm_s24le -i \"{0}\" -acodec mp3 -vcodec copy -af \"pan=mono|c0=c1\" \"{1}\"", videoPath, outVideo);
                logger.Info("... Right Mono Pipe for {0}", videoPath);
            }
            else
            {
                args_proto.AppendFormat("-y -acodec pcm_s24le -i \"{0}\" -acodec mp3 -vcodec copy \"{1}\"", videoPath, outVideo);
                logger.Info("... Stereo Pipe for {0}", videoPath);
            }

            string args = args_proto.ToString();

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    FileName = "ffmpeg.exe",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = args
                }
            };

            process.ErrorDataReceived += (sender, eventArgs) =>
            {
                logger.Error(eventArgs);
                MessageBox.Show(eventArgs.Data);
            };

            process.Start();

            process.WaitForExit();

            logger.Info("Conversion Done: {0}", videoPath);

            File.Delete(videoPath);

            File.Move(outVideo, videoPath);
        }
       */
        //download an mp4 from a remote server
        //uri - connection to download from
        //ftpFileName - file path at target remote server
        //localFilePath - file path at local
        private void DownloadFileFTP(FilePath remotePath, FilePath localFilePath)
        {
            logger.Info("Copying {0} from hyperdeck", localFilePath);
            progress++;
            SetProgress(progress);
            string ftpfullpath = remotePath.Replace(".mcc", ".mp4");
            
            using (WebClient request = new WebClient())
            {
                
                request.DownloadFile(ftpfullpath, localFilePath);

                //using (FileStream file = File.Create(inputfilepath))
                //{

                //    file.Write(fileData, 0, fileData.Length);
                //    file.Close();
                //}
            }
            progress++;
            SetProgress(progress);
            logger.Info("Done Copying {0} from hyperdeck", localFilePath);
        }

        //upload an mp4 to a remote server
        //uri - connection and file path at remote to upload to
        //filePath - file path at local to upload from
        /* public void UploadFileFTP(URI uri, FilePath filePath)
        {
            logger.Info("Copying {0} to remote", filePath);
            progress++;
            SetProgress(progress);
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Credentials = new NetworkCredential("FTP_User", "");
                    client.UploadFile(uri, WebRequestMethods.Ftp.UploadFile, filePath);
                }
                catch
                {
                    if (filePath.Contains(fileNameWide))
                    {
                        wideFTPUploadFail = true;
                    }
                    else
                    {
                        programFTPUploadFail = true;
                    }
                }
            }
            progress++;
            SetProgress(progress);
            logger.Info("Done Copying {0} to remote", filePath);
        }
*/
        //delete a file at a remote server
        //uri - remote server to delete at
        //filename - 
        private void DeleteFTPFile(URI uri, string filename)
        {
            string fullDir = uri + "/" + filename;
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(fullDir);
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            response.Close();
        }

        private List<string> GetFTPFiles(URI uri)
        {
            progress++;
            SetProgress(progress);
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());

            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }

            streamReader.Close();
            progress++;
            SetProgress(progress);
            return directories;
        }
        #endregion

        private void recordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult settingsResult = frmRecordingSetting.ShowDialog();
            if (settingsResult == DialogResult.OK)
            {
                strIPAddressPROGRAM = frmRecordingSetting.IPAddressPROGRAM;
                strIPAddressWIDE = frmRecordingSetting.IPAddressWIDE;
                strIPAddressPC = frmRecordingSetting.IPAddressPC;
                strBaseDir = frmRecordingSetting.BaseDir;
                strYear = frmRecordingSetting.Year;

                // It is possible that the base directory structure changed. Go make a new one if needed.
                CreateBaseDirectoryStructure();

                UpdateRegistryKeys();
                UpdateTBA();   // Year could have changed. 
            }
        }

        private void radioBtnMatchType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if(btn.Checked == true)
            {
                numMatchNumber.Value = 1;
                numFinalNo.Value = 1;

                lblFinalNo.Visible = true;
                numFinalNo.Visible = true;
                lblMatchNumber.Visible = true;
                numMatchNumber.Visible = true;
                lblCeremonyTitle.Visible = false;
                txtCeremonyTitle.Visible = false;

                switch (btn.Text)
                {
                    case "Qualification":
                        currentMatchType = MatchType.Qualification;
                        lblFinalNo.Visible = false;
                        numFinalNo.Visible = false;
                        numMatchNumber.Maximum = 200;
                        numFinalNo.Maximum = 1;
                        break;
                    case "Quarterfinal":
                        currentMatchType = MatchType.Quarterfinal;
                        numMatchNumber.Maximum = 3;
                        numFinalNo.Maximum = 4;
                        break;
                    case "Semifinal":
                        currentMatchType = MatchType.Semifinal;
                        numMatchNumber.Maximum = 200;
                        numFinalNo.Maximum = 16;
                        lblMatchNumber.Visible = false;
                        numMatchNumber.Visible = false;
                        break;
                    case "Final":
                        currentMatchType = MatchType.Final;
                        lblFinalNo.Visible = false;
                        numFinalNo.Visible = false;
                        numMatchNumber.Maximum = 4;
                        numFinalNo.Maximum = 1;
                        break;
                    case "Ceremony":
                        currentMatchType = MatchType.Ceremony;
                        lblMatchNumber.Visible = false;
                        numMatchNumber.Visible = false;
                        lblFinalNo.Visible = false;
                        numFinalNo.Visible = false;
                        lblCeremonyTitle.Visible = true;
                        txtCeremonyTitle.Visible = true;
                        lblCeremonyTitle.Location = new Point(10, 70);
                        txtCeremonyTitle.Location = new Point(100, 70);
                        txtCeremonyTitle.Size = new Size(280, 20);
                        numMatchNumber.Maximum = 1;
                        numFinalNo.Maximum = 1;
                        break;
                    default:
                        break;
                }
                GetMatches();
            }
        }

        private void timerElapsed_Tick(object sender, EventArgs e)
        {
            lblElapsedTime.Text = (DateTime.Now - startTime).ToString(@"hh\:mm\:ss\.ff");
        }

        private void comboEventName_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < eventDetails.Count; i++)
            {
                if (comboEventName.SelectedItem.ToString().Contains(eventDetails[i].first_event_code))
                {
                    currentEvent = eventDetails[i];

                    programVideoTitle = currentEvent.year + " " + currentEvent.name + " " + matchType + " " + numMatchNumber.Value;
                    wideVideoTitle = currentEvent.year + " " + currentEvent.name + " WIDE " + matchType + " " + numMatchNumber.Value;
                    GetMatches();
                    groupMatch.Enabled = true;
                }
                else
                {
                    // Custom Event

                    programVideoTitle = "<CUSTOM EVENT> " + matchType + " " + numMatchNumber.Value;
                    wideVideoTitle = "<CUSTOM EVENT> WIDE " + matchType + " " + numMatchNumber.Value;
                    groupMatch.Enabled = true;
                }
            }
        }

        private void GetMatches()
        {
            if (currentEvent == null || currentEvent.key == null) return;

            logger.Info("Getting Match List from TBA");
            tbaRequest = new RestRequest(string.Format("event/{0}/matches/simple", currentEvent.key), Method.GET);

            tbaRequest.AddHeader
            (
                "X-TBA-Auth-Key",
                TBAKEY
            );

            IRestResponse tbaResponse = tbaClient.Execute(tbaRequest);
            string tbaContent = tbaResponse.Content;

            // Update the matches variable

            matches = JsonConvert.DeserializeObject<Match[]>(tbaContent);
            logger.Info("Done Getting Match List from TBA");
        }
        
        private void btnConnectProgram_Click(object sender, EventArgs e)
        {
            logger.Info("Connecting to Program Hyperdeck");
            try
            {
                hdProgram = new HyperDeck(strIPAddressPROGRAM, Convert.ToInt32(strPortPROGRAM));
                logger.Info("... Status:\n{0}", hdProgram.Read());
                
                btnConnectProgram.BackColor = Color.Green;
                logger.Info("- Done");
            }
            catch (Exception ue)
            {
                logger.Error(ue);
                logger.Error("- Failed");
                MessageBox.Show(string.Format("Could not connect to the Program recorder\nat the IP address: {0}", strIPAddressPROGRAM));
            }
        }

        private void btnConnectWide_Click(object sender, EventArgs e)
        {
            logger.Info("Connecting to Wide");
            try
            {
                hdWide = new HyperDeck(strIPAddressWIDE, Convert.ToInt32(strPortWIDE));
                logger.Info("... Status:\n{0}", hdWide.Read());

                btnConnectWide.BackColor = Color.Green;
                logger.Info("- Done");
            }
            catch (Exception ue)
            {
                logger.Error(ue);
                logger.Error("- Failed");
                MessageBox.Show(string.Format("Could not connect to the Wide recorder\nat the IP address: {0}", strIPAddressWIDE));
            }
        }

        private void btnConnectPC_Click(object sender, EventArgs e)
        {
            logger.Info("Connecting to PC");
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(strIPAddressPC);
                PCPingable = reply.Status == IPStatus.Success;
            }
            catch (PingException pe)
            {
                logger.Error(pe);
                
                btnConnectPC.BackColor = Color.Green;
                groupEvent.Enabled = true;
                btnStartRecording.Enabled = true;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            if (PCPingable)
            {
                btnConnectPC.BackColor = Color.Green;
                groupEvent.Enabled = true;
                btnStartRecording.Enabled = true;
                logger.Info("- Done");
            }
            else
            {
                btnConnectPC.BackColor = Color.Red;
                groupEvent.Enabled = false;
                btnStartRecording.Enabled = false;
                MessageBox.Show("Could not connect to the PC.  Please check the IP address is correct.");
                logger.Error("- Failed");
            }
        }
        private bool MoveVersionTo(string strSourcePath, string strDestPath)
        {
            if (System.IO.File.Exists(strDestPath))
            {
                int i;
                string strNewPath = "";

                for (i = 2; i < 1000; i++)
                {
                    string strDestJustThePathPart = System.IO.Path.GetDirectoryName(strDestPath);

                    if (strDestJustThePathPart == null)
                    {
                        strDestJustThePathPart = "";
                    }

                    string strBaseFilename = System.IO.Path.GetFileNameWithoutExtension(strDestPath);
                    string strExtension = System.IO.Path.GetExtension(strDestPath);
                    strNewPath = System.IO.Path.Combine(strDestJustThePathPart, strBaseFilename + "-" + i.ToString() + strExtension);

                    if (System.IO.File.Exists(strNewPath))
                    {
                        // New path is viable. Use it
                        continue;
                    }
                    break;
                }
                if (i >= 1000)
                {
                    // Something is wrong. We were unlikely to do 1000 new files! 
                    logger.Info("Too many iterations of " + strDestPath);
                    return false;

                }
                // Change filename
                strDestPath = strNewPath;
            }
            try
            {
                System.IO.File.Move(strSourcePath, strDestPath);
            }
            catch (Exception ex)
            {
                logger.Error("Exception caught moving file");
                logger.Error(ex.ToString());
                return false;
            }
            return true;

            
        }
        #region Background Workers
        private void bgWorker_FTP_Wide_DoWork(object sender, DoWorkEventArgs e)
        {
            logger.Info("Starting Wide worker");
            logger.Info("... Wide worker waiting");
            lblReportA.Invoke((Action)(() => { lblReportA.Text = "Waiting"; }));

            // Found through experimentation that a delay is required here. 
            // I think that the Hyperdeck requires time to finalize the file. 
            // Previous 1 second now requires 1.5 sec or more


            Thread.Sleep(2500);



            progress++;
            SetProgress(progress);


            //
            //  Check/Create directory structure in BaseDir
            //

            string strYearDir = System.IO.Path.Combine(strBaseDir, strYear);
            string strEventDir = System.IO.Path.Combine(strYearDir, currentEvent.short_name);
            string strWideDir = System.IO.Path.Combine(strEventDir, "WIDE");

            

            if (!Directory.Exists(strYearDir))
            {
                logger.Info("... Creating " + strYearDir);
                Directory.CreateDirectory(strYearDir);
            }
            if (!Directory.Exists(strEventDir))
            {
                logger.Info("... Creating " + strEventDir);
                Directory.CreateDirectory(strEventDir);
            }
            if (!Directory.Exists(strWideDir))
            {
                logger.Info("... Creating " + strWideDir);
                Directory.CreateDirectory(strWideDir);
            }

            URI wideURI = string.Format("ftp://{0}/1", strIPAddressWIDE);
            FilePath widePath = string.Format("ftp://{0}/{1}/{2}/WIDE", strIPAddressPC,strYear, currentEvent.short_name);
           
            progress++;
            SetProgress(progress);

            lblReportA.Invoke((Action)(()=> { lblReportA.Text = "Finding Video"; }));
            logger.Info("... Finding Wide source file");

 
            string strDestPath = System.IO.Path.Combine(strWideDir, fileNameWide);
            string tempFile = tempFolder + "\\" + fileNameWide;
            string wideFullPath = wideURI + "/" + matchABVWide + ".mp4";

            lblReportA.Invoke((Action)(()=> { lblReportA.Text = "Downloading Video"; }));
            logger.Info("... Wide worker downloading source");
            DownloadFileFTP(wideFullPath, tempFile);

            //lblReportA.Invoke((Action)(()=> { lblReportA.Text = "Converting Video"; }));
            //logger.Info("... Wide worker processing");
            //ConvertVideo(tempFile, wideChannels);

            // lblReportA.Invoke((Action)(()=> { lblReportA.Text = "Uploading Video"; }));
            // logger.Info("... Wide worker uploading");
            // UploadFileFTP(widePath + "/" + fileNameWide, tempFile);

            // Now move the file from the temp directory to the final resting place
                       
            try
            {
                if(MoveVersionTo(tempFile, strDestPath))
                {
                    // If that succeded, then remove the file from the HyperDeck
                    DeleteFTPFile(wideURI, matchABVWide + ".mp4");
                }
 

            } catch 
            {
                string errorMessage = "Unable to move " + tempFile + " to directory " + strDestPath;
                logger.Info(errorMessage);
                lblReportA.Invoke((Action)(() => { lblReportA.Text = errorMessage; }));
                return;
            }

  

            logger.Info("- Done: Wide");
            lblReportA.Invoke((Action)(()=> { lblReportA.Text = "Done"; }));

            ledWide.BackColor = Color.Green;
        }

        private void bgWorker_FTP_Program_DoWork(object sender, DoWorkEventArgs e)
        {
            logger.Info("Starting Program worker");
            logger.Info("... Program worker waiting");
            lblReportB.Invoke((Action)(()=> { lblReportB.Text = "Waiting"; }));

            // Found through experimentation that a delay is required here. 
            // I think that the Hyperdeck requires time to finalize the file. 
            // Previous 1 second now requires 1.5 sec or more

            
            Thread.Sleep(2500);

            logger.Info("... Program cleaning HyperDeck SD");
            lblReportB.Invoke((Action)(()=> { lblReportB.Text = "Clearing SD"; }));

            progress++;
            SetProgress(progress);
            URI programURI = string.Format("ftp://{0}/1", strIPAddressPROGRAM);
            FilePath programPath = string.Format("ftp://{0}/{1}/{2}/PROGRAM", strIPAddressPC,strYear, currentEvent.short_name);

            //
            //  Check/Create directory structure in BaseDir
            //

            string strYearDir = System.IO.Path.Combine(strBaseDir, strYear);
            string strEventDir = System.IO.Path.Combine(strYearDir, currentEvent.short_name);
            string strProgDir = System.IO.Path.Combine(strEventDir, "PROGRAM");



            if (!Directory.Exists(strYearDir))
            {
                logger.Info("... Creating " + strYearDir);
                Directory.CreateDirectory(strYearDir);
            }
            if (!Directory.Exists(strEventDir))
            {
                logger.Info("... Creating " + strEventDir);
                Directory.CreateDirectory(strEventDir);
            }
            if (!Directory.Exists(strProgDir))
            {
                logger.Info("... Creating " + strProgDir);
                Directory.CreateDirectory(strProgDir);
            }

          
            progress++;
            SetProgress(progress);

            lblReportB.Invoke((Action)(()=> { lblReportB.Text = "Downloading Video"; }));
            logger.Info("... Finding Program source file");


            string strDestPath = System.IO.Path.Combine(strProgDir, fileNameProgram);
            string tempFile = tempFolder +"\\"+ fileNameProgram;
            string programFullPath = programURI + "/" + matchABVProgram + ".mp4";
            logger.Info("... Program worker downloading source " + programFullPath + " to " + tempFile);
            lblReportB.Invoke((Action)(() => { lblReportB.Text = "Downloading Video"; }));



            DownloadFileFTP(programFullPath, tempFile);

           

            try
            {
                if (MoveVersionTo(tempFile, strDestPath))
                {
                    // If that succeded, then remove the file from the HyperDeck
                    DeleteFTPFile(programURI, matchABVProgram + ".mp4");
                }

            }
            catch
            {
                string errorMessage = "Unable to move " + tempFile + " to directory " + strDestPath;
                logger.Info(errorMessage);
                lblReportA.Invoke((Action)(() => { lblReportA.Text = errorMessage; }));
                return;
            }

            logger.Info("- Done: Program");
            lblReportB.Invoke((Action)(()=> { lblReportB.Text = "Done"; }));

           

            ledProgram.BackColor = Color.Green;
        }

        private void bgWorker_WD_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            while(state == FormState.Recording)
            {
                hdProgram.Write("transport info");
                string progStatus = hdProgram.Read();

                hdWide.Write("transport info");
                string wideStatus = hdWide.Read();
                
                logger.Info("Live Record Status:");
                logger.Info(wideStatus);
                logger.Info(progStatus);

                if (!wideStatus.Contains("record"))
                {
                    btnConnectWide.BackColor = Color.Yellow;
                }
                if (!progStatus.Contains("record"))
                {
                    btnConnectProgram.BackColor = Color.Yellow;
                }
                Thread.Sleep(1000);
            }
        }

        private void btnShowYT_Click(object sender, EventArgs e)
        {
            YoutubeUpload YTForm = new YoutubeUpload(
                                    fileNameProgram,
                                    fileNameWide,
                                    ytDescription,
                                    ytTags);
            YTForm.StartPosition = FormStartPosition.CenterParent;
            YTForm.Show();
        }

        private void version001ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPage about = new AboutPage();
            about.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage help = new HelpPage();
            help.Show();
        }

        private void launch_youtube()
        {
            if (currentMatch != null)
            {
                ytDescription = string.Format("{0} FRC {1} Week #{2}\n" +
                           "Red Alliance: {3} {4} {5}\n" +
                           "Blue Alliance: {6} {7} {8}\n\n" +
                           "Footage of the {0} FRC {1} is coutesy of the FIRST Washington A/V Crew\n\n" +
                           //"To view match schedules and results for this event, visit the FRC Event Results Portal:\n" +
                           //"{9}\n\n" +
                           "Folow the PNW District social media accounts for updates throughout the season!\n" +
                           "Facebook: Washington FIRST Robotics / OregonFRC\n" +
                           "Twitter: @first_wa / @OregonRobotics\n" +
                           "Youtube: Washington FIRST Robotics\n\n" +
                           "For more information and future event schedules, visit our websites:\n" +
                           "http://www.firstwa.org | http://www.oregonfirst.org \n\n" +
                           "Thanks for watching!",
                           currentEvent.year,
                           currentEvent.name,
                           currentEvent.week + 1,
                           currentMatch.Alliances.Red.TeamKeys[0].ToString().Substring(3),
                           currentMatch.Alliances.Red.TeamKeys[1].ToString().Substring(3),
                           currentMatch.Alliances.Red.TeamKeys[2].ToString().Substring(3),
                           currentMatch.Alliances.Blue.TeamKeys[0].ToString().Substring(3),
                           currentMatch.Alliances.Blue.TeamKeys[1].ToString().Substring(3),
                           currentMatch.Alliances.Blue.TeamKeys[2].ToString().Substring(3));
            }
            else
            {
                MessageBox.Show("Please enter team numbers in the description template.");
                ytDescription = string.Format("{0} FRC {1} Week #{2}\n" +
                           "Red Alliance: [RED 1] [RED 2] [RED3]\n" +
                           "Blue Alliance: [BLUE 1] [BLUE 2] [BLUE 3]\n\n" +
                           "Footage of the {0} FRC {1} is courtesy of the FIRST Washington A/V Crew\n\n" +
                           //"To view match schedules and results for this event, visit the FRC Event Results Portal:\n" +
                           //"{9}\n\n" +
                           "Follow the PNW District social media accounts for updates throughout the season!\n" +
                           "Facebook: FIRST Washington / FIRSToregon\n" +
                           "Twitter: @first_wa / @FIRST_Oregon\n" +
                           "Youtube: FIRST Washington\n\n" +
                           "For more information and future event schedules, visit our websites:\n" +
                           "https://firstwa.org | https://ortop.org \n\n" +
                           "Thanks for watching!",
                           currentEvent.year,
                           currentEvent.name,
                           currentEvent.week + 1);
            }
            

            ytTags = "first,robotics,frc," + currentEvent.year.ToString() + "," + currentEvent.event_code;


            YoutubeUpload YTForm = new YoutubeUpload(
                                    fileNameProgram,
                                    fileNameWide,
                                    ytDescription,
                                    ytTags);
            YTForm.StartPosition = FormStartPosition.CenterParent;
            YTForm.Show();
            btnShowYT.Enabled = true;
        }

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult settingsResult = frmAudioSetting.ShowDialog();
            if (settingsResult == DialogResult.OK)
            {
                wideChannels = frmAudioSetting.wide;
                progChannels = frmAudioSetting.prog;

                UpdateRegistryKeys();
            }
        }

        private void txtCeremonyTitle_Leave(object sender, EventArgs e)
        {
            Regex sanatize = new Regex("[^a-zA-Z0-9_ ]");
            txtCeremonyTitle.Text = sanatize.Replace(txtCeremonyTitle.Text.ToString(), "");
        }

        private void bgWorker_FTP_Program_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgWorker_FTP_Wide.IsBusy)
            {
                SetProgress(progressBar1.Maximum);
                state = FormState.Idle;
                if (bYoutubePopup) launch_youtube();
            }
        }

        private void btnTempAccess_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(tempFolder);
            }
            catch(Exception ue)
            {
                logger.Error("Failed to access temp folder for user");
                logger.Info(ue.Message);
            }
        }

        private void bgWorker_FTP_Wide_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!bgWorker_FTP_Program.IsBusy)
            {
                SetProgress(progressBar1.Maximum);
                state = FormState.Idle;
                if (bYoutubePopup)  launch_youtube();
            }

        }

        private void lblCeremonyTitle_Click(object sender, EventArgs e)
        {

        }

        private void checkYTPopup_CheckedChanged(object sender, EventArgs e)
        {
            bYoutubePopup = checkYTPopup.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("WARNING!  This operation will ccancel the file copy process!\n\nDo you want to continue?", "Warning!", MessageBoxButtons.YesNo);

            if (r == DialogResult.Yes)
            {
                if (bgWorker_FTP_Program.IsBusy)
                {
                    bgWorker_FTP_Program.CancelAsync();
                }

                if (bgWorker_FTP_Wide.IsBusy)
                {
                    bgWorker_FTP_Wide.CancelAsync();
                }
            }
            btnCancel.Enabled = false;
            state = FormState.Idle;
        }
        #endregion

        private void checkHackData_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTBA();
        }

        private void btnOpenRecordings_Click(object sender, EventArgs e)
        {
        }

        //youtube upload handler
        //

        #region Callbacks
        delegate void SetProgressCallback(int progress);
        private void SetProgress(int progress)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBar1.InvokeRequired)
            {
                SetProgressCallback d = new SetProgressCallback(SetProgress);
                this.Invoke(d, new object[] { progress });
            }
            else
            {
                this.progressBar1.Value = progress;
            }

        }
        #endregion
    


    }
}

    
