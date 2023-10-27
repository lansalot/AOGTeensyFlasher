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
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnProgram.Enabled = false;
        }

        private void lbFirmware_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(localCSV);
            var secondColumn = lines
                .Where(line => line.Split(',')[0] == (String)lbFirmware.SelectedValue)
                .Select(line => line.Split(',')[1])
                .ToList();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://raw.githubusercontent.com/lansalot/AOGTeensyFlasher/main/Firmwares.csv";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, localCSV);
                }
                txtMessages.Text += "Firmware list downloaded!\r\n";

                // Read the CSV file and fill the listbox with the first column
                var lines = File.ReadAllLines(localCSV);
                var firstColumn = lines.Select(line => line.Split(',')[0]).ToList();
                lbFirmware.DataSource = firstColumn;

            }
            catch
            {
                txtMessages.Text += "Error downloading firmware list\r\n";
            }
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            var teensy = lbTeensies.SelectedItem as ITeensy;
            //if (teensy != null)
            //{
            //    string filename = tbHexfile.Text;
            //    if (File.Exists(filename))
            //    {
            //        var progress = new Progress<int>(v => progressBar.Value = v);
            //        progressBar.Visible = true;
            //        var result = await teensy.UploadAsync(filename, progress);
            //        MessageBox.Show(result.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        progressBar.Visible = false;
            //        progressBar.Value = 0;
            //    }
            //    else MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
