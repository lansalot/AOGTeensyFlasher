using libTeensySharp;
using lunOptics.libTeensySharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TeensyFlasher
{
    public partial class frmMain : Form
    {
        TeensyWatcher watcher;
        String localCSV = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Firmwares.csv");
        string localHexStub = AppDomain.CurrentDomain.BaseDirectory;
        string chosenFirmware = "";

        void LogMessage(string Text)
        {
            txtMessages.Text += Text + "\r\n";
            txtMessages.SelectionStart = txtMessages.Text.Length;
            txtMessages.ScrollToCaret();
        }
        void UpdateFirmwareBox()
        {
            // Read the CSV file and fill the listbox with the first column
            var lines = File.ReadAllLines(localCSV);
            var firstColumn = lines.Select(line => line.Split(',')[0]).ToList();
            lbFirmware.DataSource = firstColumn;
            lbFirmware.SelectedIndex = -1;
        }
        public frmMain()
        {
            InitializeComponent();
            watcher = new TeensyWatcher(SynchronizationContext.Current);
            watcher.ConnectedTeensies.CollectionChanged += ConnectedTeensiesChanged;
            foreach (var teensy in watcher.ConnectedTeensies)
            {
                lbTeensies.Items.Add(teensy);
            }
            if (lbTeensies.Items.Count > 0) lbTeensies.SelectedIndex = 0;
            if (File.Exists(localCSV))
            {
                UpdateFirmwareBox();
            }
        }

        private void ConnectedTeensiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var teensy in e.NewItems)
                    {
                        lbTeensies.Items.Add(teensy);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var teensy in e.OldItems)
                    {
                        lbTeensies.Items.Remove(teensy);
                    }
                    break;
            }
            if (lbTeensies.SelectedIndex == -1 && lbTeensies.Items.Count > 0) lbTeensies.SelectedIndex = 0;
            if (lbTeensies.Items.Count == 0)
            {
                btnProgram.Enabled = false;
            } else
            {
                if (lbFirmware.SelectedIndex > -1)
                {
                    btnProgram.Enabled = true;
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnProgram.Enabled = false;
        }

        private void lbFirmware_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(localCSV);
            chosenFirmware = lines
                .Where(line => line.Split(',')[0] == (String)lbFirmware.SelectedValue)
                .Select(line => line.Split(',')[1])
                .FirstOrDefault();
            if (lbFirmware.SelectedIndex > -1 && lbTeensies.SelectedIndex > -1 && lbTeensies.Items.Count > 0)
            {
                btnProgram.Enabled = true;
            }
        }

        bool DownloadFile(string url, string localFile)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, localFile);
                    LogMessage("Downloaded " + localFile);
                    return true;
                }
            }
            catch
            {
                LogMessage("Error downloading " + localFile);
                return false;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string url = "https://raw.githubusercontent.com/lansalot/AOGTeensyFlasher/main/Firmwares.csv";
            DownloadFile(url, localCSV);
            UpdateFirmwareBox();
        }

        private async void btnProgram_Click(object sender, EventArgs e)
        {
            string localHexFile = System.IO.Path.Combine(localHexStub, Path.GetFileName(chosenFirmware));
            if (!File.Exists(localHexFile))
            {
                LogMessage("Firmware file not found locally.. downloading");
                if (!DownloadFile(chosenFirmware, localHexFile)) return;
            }
            if ((lbTeensies.SelectedIndex == -1) | (lbTeensies.Items.Count == 0))
            {
                LogMessage("Sorry, no Teensies selected to program");
                return;
            }
            LogMessage("Programming!");
            var teensy = lbTeensies.SelectedItem as ITeensy;
            if (teensy != null)
            {
                var progress = new Progress<int>(v => pbProgram.Value = v);
                var result = await teensy.UploadAsync(localHexFile, progress);
                if (result.ToString() == "RebootError")
                {
                    LogMessage("Couldn't reboot Teensy - try pressing the white button on it to trigger download and press Program again");
                }
                else if (result.ToString() != "OK")
                {
                    LogMessage("Error: " + result.ToString());
                    LogMessage("Are you sure nothing else is using the Teensy?");
                    LogMessage("Close the Arduino IDE and try again, or unplug/reinsert the Teensy");
                }
                else
                {
                    LogMessage("Finished programming");
                }
                pbProgram.Value = 0;
            }
        }
    }
}
