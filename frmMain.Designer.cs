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
            this.lbTeensy = new System.Windows.Forms.ListBox();
            this.lbFirmware = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProgram = new System.Windows.Forms.Button();
            this.btnREfresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTeensy
            // 
            this.lbTeensy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTeensy.FormattingEnabled = true;
            this.lbTeensy.ItemHeight = 25;
            this.lbTeensy.Location = new System.Drawing.Point(523, 96);
            this.lbTeensy.Name = "lbTeensy";
            this.lbTeensy.Size = new System.Drawing.Size(358, 179);
            this.lbTeensy.TabIndex = 0;
            // 
            // lbFirmware
            // 
            this.lbFirmware.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFirmware.FormattingEnabled = true;
            this.lbFirmware.ItemHeight = 25;
            this.lbFirmware.Location = new System.Drawing.Point(12, 96);
            this.lbFirmware.Name = "lbFirmware";
            this.lbFirmware.Size = new System.Drawing.Size(358, 179);
            this.lbFirmware.TabIndex = 1;
            this.lbFirmware.SelectedIndexChanged += new System.EventHandler(this.lbFirmware_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "1. Select a firmware";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(517, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "2. Pick your Teensy";
            // 
            // btnProgram
            // 
            this.btnProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgram.Location = new System.Drawing.Point(523, 288);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(140, 45);
            this.btnProgram.TabIndex = 4;
            this.btnProgram.Text = "Program!";
            this.btnProgram.UseVisualStyleBackColor = true;
            // 
            // btnREfresh
            // 
            this.btnREfresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnREfresh.Location = new System.Drawing.Point(12, 281);
            this.btnREfresh.Name = "btnREfresh";
            this.btnREfresh.Size = new System.Drawing.Size(140, 52);
            this.btnREfresh.TabIndex = 5;
            this.btnREfresh.Text = "Refresh list";
            this.btnREfresh.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 590);
            this.Controls.Add(this.btnREfresh);
            this.Controls.Add(this.btnProgram);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbFirmware);
            this.Controls.Add(this.lbTeensy);
            this.Name = "frmMain";
            this.Text = "agOpenGPS Teensy Flasher";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox lbTeensy;
        private ListBox lbFirmware;
        private Label label1;
        private Label label2;
        private Button btnProgram;
        private Button btnREfresh;
    }
}

