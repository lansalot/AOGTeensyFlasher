using libTeensySharp;
using lunOptics.libTeensySharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
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
        private byte[] _ubxParseBuffer = new byte[10];
        private int _ubxParseIndex = 0;


        #region Teensy


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
            }
            else
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
        #endregion


        #region UBlox

        private SerialPort _serialPort = null;
        private Thread dataReadingThread;
        private bool isReadingData = false;

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (isReadingData)
                {
                    // This event is called when data is received on the serial port
                    string receivedData = _serialPort.ReadLine(); // Read the data from serial port
                    if (receivedData.Contains("FWVER"))
                    {
                        var txt = receivedData.Substring(receivedData.IndexOf("FWVER"), 14);
                        BeginInvoke(new Action(() =>
                        {
                            lblFirmware.Text = txt;
                        }));
                    }

                    PrintHexAndAscii(receivedData);
                    // Update the text box asynchronously
                    BeginInvoke(new Action(() =>
                    {
                        if (receivedData.StartsWith("$GN"))
                        {
                            txtSerialChat.AppendText(receivedData + Environment.NewLine);
                        }
                    }));
                }
            }
            catch
            {
                Debug.WriteLine("!");
            }
        }

        private void StartReadingData()
        {
            isReadingData = true;
            dataReadingThread = new Thread(() =>
            {
                while (isReadingData)
                {
                    try
                    {
                        if (!_serialPort.IsOpen)
                            _serialPort.Open(); // Open the serial port if it's not already open
                    }
                    catch (Exception ex)
                    {
                        txtSerialChat.AppendText("Error opening serial port: " + ex.Message + Environment.NewLine);
                        return;
                    }

                    // Read data continuously
                    // The SerialPort_DataReceived event will be called when data is received
                }
            });
            dataReadingThread.Start();
        }

        private void StopReadingData()
        {
            isReadingData = false;
            _serialPort.DataReceived -= SerialPort_DataReceived;
            if (dataReadingThread != null && dataReadingThread.IsAlive)
            {
                dataReadingThread.Join(); // Wait for the thread to stop
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.Close(); // Close the serial port
            }
        }
        private void btnURefresh_Click(object sender, EventArgs e)
        {

            var ports = SerialPort.GetPortNames().ToList();
            lbCOMPorts.Items.Clear();
            foreach (var port in ports)
            {
                lbCOMPorts.Items.Add(port);
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (isReadingData)
            {
                StopReadingData();
                btnConnect.Text = "Connect";
            }
            else
            {
                _serialPort = new SerialPort(lbCOMPorts.SelectedItem.ToString(), 460800, Parity.None, 8, StopBits.One);
                btnConnect.Text = "Disconnect";
                _serialPort.DataReceived += SerialPort_DataReceived;

                StartReadingData();
            }
        }

        private void lbCOMPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCOMPorts.SelectedIndex > -1)
            {
                btnConnect.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = false;
            }
        }

        #endregion
        #region ubloxhelpers
        private void ResetUbxBuffer()
        {
            for (var i = 0; i < _ubxParseBuffer.Length; i++)
            {
                _ubxParseBuffer[i] = 0;
            }

            _ubxParseIndex = 0;
        }

        private void UbxCalculateCheckSum(byte[] msg)
        {
            byte ckA = 0;
            byte ckB = 0;

            for (int i = 2; i < msg.Length - 2; i++)
            {
                ckA = (byte)(ckA + msg[i]);
                ckB = (byte)(ckB + ckA);

                //System.Diagnostics.Debug.WriteLine($"Calculating : {i}");
            }

            msg[msg.Length - 2] = ckA;
            msg[msg.Length - 1] = ckB;
        }

        public static void PrintHexAndAscii(string input)
        {
            for (int i = 0; i < input.Length; i += 30)
            {
                string hexSection = "";
                string asciiSection = "";

                // Process each character in the section
                for (int j = i; j < i + 30 && j < input.Length; j++)
                {
                    // Convert each character to its hexadecimal representation
                    string hex = ((int)input[j]).ToString("X2");
                    hexSection += hex + " ";

                    // Convert non-printable characters to a dot for readability
                    char printableAscii = char.IsControl(input[j]) || input[j] > 127 ? '.' : input[j];
                    asciiSection += printableAscii;
                }

                // Print the sections
                Console.WriteLine($"{hexSection,-41} {asciiSection}");
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (isReadingData)
            { // B5 62 0A 04 00 00 0E 34
                //byte[] ubxMsg = { 0x0a, 0x04, 0x00, 0x00};
                byte[] ubxMsg = { 0xB5, 0x62, 0x0A, 0x04, 0x00, 0x00, 0x0E, 0x34 };
                //UbxCalculateCheckSum(ubxMsg);
                // can only get going if port is open, good luck finding the response
                if (_serialPort.IsOpen)
                {
                    // array of 4 bytes
                    _serialPort.Write(ubxMsg,0,ubxMsg.Length); // Send test string down the serial port
                }
                else
                {
                    txtSerialChat.AppendText("Serial port is not open." + Environment.NewLine);
                }
            }
        }

        private static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
        #endregion


        private void tbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (isReadingData)
            {
                StopReadingData();
                _serialPort.Dispose();

            }
            base.OnFormClosing(e);
        }

    }
}
