using libTeensySharp;
using lunOptics.libTeensySharp;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace TeensyFlasher
{
    partial class frmMain
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
            this.tbPages = new System.Windows.Forms.TabControl();
            this.tabTeensy = new System.Windows.Forms.TabPage();
            this.lbTeensies = new System.Windows.Forms.ListBox();
            this.pbProgram = new System.Windows.Forms.ProgressBar();
            this.lblMessages = new System.Windows.Forms.Label();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFirmware = new System.Windows.Forms.ListBox();
            this.tabGPS = new System.Windows.Forms.TabPage();
            this.lblUblox = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnURefresh = new System.Windows.Forms.Button();
            this.lbCOMPorts = new System.Windows.Forms.ListBox();
            this.txtSerialChat = new System.Windows.Forms.TextBox();
            this.lblCOMPorts = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblFirmware = new System.Windows.Forms.Label();
            this.tbPages.SuspendLayout();
            this.tabTeensy.SuspendLayout();
            this.tabGPS.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPages
            // 
            this.tbPages.Controls.Add(this.tabTeensy);
            this.tbPages.Controls.Add(this.tabGPS);
            this.tbPages.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPages.Location = new System.Drawing.Point(13, 51);
            this.tbPages.Margin = new System.Windows.Forms.Padding(4);
            this.tbPages.Name = "tbPages";
            this.tbPages.SelectedIndex = 0;
            this.tbPages.Size = new System.Drawing.Size(971, 719);
            this.tbPages.TabIndex = 11;
            this.tbPages.SelectedIndexChanged += new System.EventHandler(this.tbPages_SelectedIndexChanged);
            // 
            // tabTeensy
            // 
            this.tabTeensy.Controls.Add(this.lbTeensies);
            this.tabTeensy.Controls.Add(this.pbProgram);
            this.tabTeensy.Controls.Add(this.lblMessages);
            this.tabTeensy.Controls.Add(this.txtMessages);
            this.tabTeensy.Controls.Add(this.btnRefresh);
            this.tabTeensy.Controls.Add(this.btnProgram);
            this.tabTeensy.Controls.Add(this.label2);
            this.tabTeensy.Controls.Add(this.label1);
            this.tabTeensy.Controls.Add(this.lbFirmware);
            this.tabTeensy.Location = new System.Drawing.Point(4, 45);
            this.tabTeensy.Margin = new System.Windows.Forms.Padding(4);
            this.tabTeensy.Name = "tabTeensy";
            this.tabTeensy.Padding = new System.Windows.Forms.Padding(4);
            this.tabTeensy.Size = new System.Drawing.Size(963, 670);
            this.tabTeensy.TabIndex = 0;
            this.tabTeensy.Text = "Teensy";
            this.tabTeensy.UseVisualStyleBackColor = true;
            // 
            // lbTeensies
            // 
            this.lbTeensies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTeensies.FormattingEnabled = true;
            this.lbTeensies.ItemHeight = 25;
            this.lbTeensies.Location = new System.Drawing.Point(133, 286);
            this.lbTeensies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbTeensies.Name = "lbTeensies";
            this.lbTeensies.Size = new System.Drawing.Size(475, 54);
            this.lbTeensies.TabIndex = 19;
            // 
            // pbProgram
            // 
            this.pbProgram.Location = new System.Drawing.Point(711, 351);
            this.pbProgram.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbProgram.Name = "pbProgram";
            this.pbProgram.Size = new System.Drawing.Size(229, 23);
            this.pbProgram.TabIndex = 18;
            // 
            // lblMessages
            // 
            this.lblMessages.AutoSize = true;
            this.lblMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessages.Location = new System.Drawing.Point(13, 338);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(150, 36);
            this.lblMessages.TabIndex = 17;
            this.lblMessages.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(19, 382);
            this.txtMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(921, 196);
            this.txtMessages.TabIndex = 16;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(800, 18);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 52);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh list";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnProgram
            // 
            this.btnProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgram.Location = new System.Drawing.Point(800, 282);
            this.btnProgram.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(140, 46);
            this.btnProgram.TabIndex = 14;
            this.btnProgram.Text = "Program!";
            this.btnProgram.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 44);
            this.label2.TabIndex = 13;
            this.label2.Text = "Teensy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 36);
            this.label1.TabIndex = 12;
            this.label1.Text = "1. Select a local firmware or Refresh list if none";
            // 
            // lbFirmware
            // 
            this.lbFirmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirmware.FormattingEnabled = true;
            this.lbFirmware.ItemHeight = 25;
            this.lbFirmware.Location = new System.Drawing.Point(21, 74);
            this.lbFirmware.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbFirmware.Name = "lbFirmware";
            this.lbFirmware.Size = new System.Drawing.Size(919, 204);
            this.lbFirmware.TabIndex = 11;
            // 
            // tabGPS
            // 
            this.tabGPS.Controls.Add(this.lblFirmware);
            this.tabGPS.Controls.Add(this.btnQuery);
            this.tabGPS.Controls.Add(this.btnConnect);
            this.tabGPS.Controls.Add(this.lblCOMPorts);
            this.tabGPS.Controls.Add(this.txtSerialChat);
            this.tabGPS.Controls.Add(this.lbCOMPorts);
            this.tabGPS.Controls.Add(this.btnURefresh);
            this.tabGPS.Controls.Add(this.label3);
            this.tabGPS.Controls.Add(this.lblUblox);
            this.tabGPS.Location = new System.Drawing.Point(4, 45);
            this.tabGPS.Margin = new System.Windows.Forms.Padding(4);
            this.tabGPS.Name = "tabGPS";
            this.tabGPS.Size = new System.Drawing.Size(963, 670);
            this.tabGPS.TabIndex = 1;
            this.tabGPS.Text = "Ublox";
            this.tabGPS.UseVisualStyleBackColor = true;
            // 
            // lblUblox
            // 
            this.lblUblox.AutoSize = true;
            this.lblUblox.Location = new System.Drawing.Point(20, 21);
            this.lblUblox.Name = "lblUblox";
            this.lblUblox.Size = new System.Drawing.Size(293, 36);
            this.lblUblox.TabIndex = 0;
            this.lblUblox.Text = "U-Blox Configuration";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(415, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(516, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Please ensure F9P is plugged in with USB next to antenna";
            // 
            // btnURefresh
            // 
            this.btnURefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnURefresh.Location = new System.Drawing.Point(27, 76);
            this.btnURefresh.Name = "btnURefresh";
            this.btnURefresh.Size = new System.Drawing.Size(182, 57);
            this.btnURefresh.TabIndex = 2;
            this.btnURefresh.Text = "Rescan Ports";
            this.btnURefresh.UseVisualStyleBackColor = true;
            this.btnURefresh.Click += new System.EventHandler(this.btnURefresh_Click);
            // 
            // lbCOMPorts
            // 
            this.lbCOMPorts.FormattingEnabled = true;
            this.lbCOMPorts.ItemHeight = 36;
            this.lbCOMPorts.Location = new System.Drawing.Point(26, 168);
            this.lbCOMPorts.Name = "lbCOMPorts";
            this.lbCOMPorts.Size = new System.Drawing.Size(183, 328);
            this.lbCOMPorts.TabIndex = 3;
            this.lbCOMPorts.SelectedIndexChanged += new System.EventHandler(this.lbCOMPorts_SelectedIndexChanged);
            // 
            // txtSerialChat
            // 
            this.txtSerialChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialChat.Location = new System.Drawing.Point(16, 523);
            this.txtSerialChat.Multiline = true;
            this.txtSerialChat.Name = "txtSerialChat";
            this.txtSerialChat.Size = new System.Drawing.Size(915, 132);
            this.txtSerialChat.TabIndex = 4;
            // 
            // lblCOMPorts
            // 
            this.lblCOMPorts.AutoSize = true;
            this.lblCOMPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOMPorts.Location = new System.Drawing.Point(21, 140);
            this.lblCOMPorts.Name = "lblCOMPorts";
            this.lblCOMPorts.Size = new System.Drawing.Size(110, 25);
            this.lblCOMPorts.TabIndex = 5;
            this.lblCOMPorts.Text = "Serial ports";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(254, 76);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(163, 57);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(472, 76);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(200, 57);
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblFirmware
            // 
            this.lblFirmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirmware.Location = new System.Drawing.Point(730, 76);
            this.lblFirmware.Name = "lblFirmware";
            this.lblFirmware.Size = new System.Drawing.Size(201, 57);
            this.lblFirmware.TabIndex = 8;
            this.lblFirmware.Text = "Firmware";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 785);
            this.Controls.Add(this.tbPages);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMain";
            this.Text = "agOpenGPS Teensy Flasher";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tbPages.ResumeLayout(false);
            this.tabTeensy.ResumeLayout(false);
            this.tabTeensy.PerformLayout();
            this.tabGPS.ResumeLayout(false);
            this.tabGPS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TabControl tbPages;
        private TabPage tabTeensy;
        private TabPage tabGPS;
        private ListBox lbTeensies;
        private ProgressBar pbProgram;
        private Label lblMessages;
        private TextBox txtMessages;
        private Button btnRefresh;
        private Button btnProgram;
        private Label label2;
        private Label label1;
        private ListBox lbFirmware;
        private Button btnURefresh;
        private Label label3;
        private Label lblUblox;
        private TextBox txtSerialChat;
        private ListBox lbCOMPorts;
        private Label lblCOMPorts;
        private Button btnConnect;
        private Button btnQuery;
        private Label lblFirmware;
    }
}

