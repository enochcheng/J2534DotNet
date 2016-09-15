namespace Sample
{
    partial class Form1
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
            this.cmdDetectDevices = new System.Windows.Forms.Button();
            this.txtDevices = new System.Windows.Forms.TextBox();
            this.cmdReadVoltage = new System.Windows.Forms.Button();
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.cmdReadVin = new System.Windows.Forms.Button();
            this.txtReadVin = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdDetectDevices
            // 
            this.cmdDetectDevices.Location = new System.Drawing.Point(17, 16);
            this.cmdDetectDevices.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDetectDevices.Name = "cmdDetectDevices";
            this.cmdDetectDevices.Size = new System.Drawing.Size(205, 28);
            this.cmdDetectDevices.TabIndex = 0;
            this.cmdDetectDevices.Text = "Detect J2534 Devices";
            this.cmdDetectDevices.UseVisualStyleBackColor = true;
            this.cmdDetectDevices.Click += new System.EventHandler(this.CmdDetectDevicesClick);
            // 
            // txtDevices
            // 
            this.txtDevices.Location = new System.Drawing.Point(231, 18);
            this.txtDevices.Margin = new System.Windows.Forms.Padding(4);
            this.txtDevices.Multiline = true;
            this.txtDevices.Name = "txtDevices";
            this.txtDevices.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDevices.Size = new System.Drawing.Size(571, 196);
            this.txtDevices.TabIndex = 1;
            // 
            // cmdReadVoltage
            // 
            this.cmdReadVoltage.Location = new System.Drawing.Point(16, 231);
            this.cmdReadVoltage.Margin = new System.Windows.Forms.Padding(4);
            this.cmdReadVoltage.Name = "cmdReadVoltage";
            this.cmdReadVoltage.Size = new System.Drawing.Size(207, 28);
            this.cmdReadVoltage.TabIndex = 2;
            this.cmdReadVoltage.Text = "Read Voltage";
            this.cmdReadVoltage.UseVisualStyleBackColor = true;
            this.cmdReadVoltage.Click += new System.EventHandler(this.CmdReadVoltageClick);
            // 
            // txtVoltage
            // 
            this.txtVoltage.Location = new System.Drawing.Point(231, 234);
            this.txtVoltage.Margin = new System.Windows.Forms.Padding(4);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Size = new System.Drawing.Size(571, 22);
            this.txtVoltage.TabIndex = 3;
            // 
            // cmdReadVin
            // 
            this.cmdReadVin.Location = new System.Drawing.Point(16, 267);
            this.cmdReadVin.Margin = new System.Windows.Forms.Padding(4);
            this.cmdReadVin.Name = "cmdReadVin";
            this.cmdReadVin.Size = new System.Drawing.Size(207, 28);
            this.cmdReadVin.TabIndex = 4;
            this.cmdReadVin.Text = "Read VIN";
            this.cmdReadVin.UseVisualStyleBackColor = true;
            this.cmdReadVin.Click += new System.EventHandler(this.CmdReadVinClick);
            // 
            // txtReadVin
            // 
            this.txtReadVin.Location = new System.Drawing.Point(232, 270);
            this.txtReadVin.Margin = new System.Windows.Forms.Padding(4);
            this.txtReadVin.Name = "txtReadVin";
            this.txtReadVin.Size = new System.Drawing.Size(569, 22);
            this.txtReadVin.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 303);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "ISO15765 Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 360);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(784, 156);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "CAN Rx Log";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(232, 303);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 28);
            this.button2.TabIndex = 9;
            this.button2.Text = "GMLan Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 529);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtReadVin);
            this.Controls.Add(this.cmdReadVin);
            this.Controls.Add(this.txtVoltage);
            this.Controls.Add(this.cmdReadVoltage);
            this.Controls.Add(this.txtDevices);
            this.Controls.Add(this.cmdDetectDevices);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "J2534DotNet Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDetectDevices;
        private System.Windows.Forms.TextBox txtDevices;
        private System.Windows.Forms.Button cmdReadVoltage;
        private System.Windows.Forms.TextBox txtVoltage;
        private System.Windows.Forms.Button cmdReadVin;
        private System.Windows.Forms.TextBox txtReadVin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}

