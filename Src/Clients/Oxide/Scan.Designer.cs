namespace Oxide
{
    partial class Scan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scan));
            this.Imager = new System.Windows.Forms.PictureBox();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.CameraTag = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorTag = new System.Windows.Forms.Label();
            this.MainRefresh = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Imager)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Imager
            // 
            this.Imager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Imager.Location = new System.Drawing.Point(0, 0);
            this.Imager.Name = "Imager";
            this.Imager.Size = new System.Drawing.Size(650, 980);
            this.Imager.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Imager.TabIndex = 2;
            this.Imager.TabStop = false;
            // 
            // Clock
            // 
            this.Clock.Interval = 1000;
            this.Clock.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CameraTag
            // 
            this.CameraTag.AutoSize = true;
            this.CameraTag.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CameraTag.ForeColor = System.Drawing.Color.White;
            this.CameraTag.Location = new System.Drawing.Point(372, 11);
            this.CameraTag.Name = "CameraTag";
            this.CameraTag.Size = new System.Drawing.Size(23, 20);
            this.CameraTag.TabIndex = 0;
            this.CameraTag.Text = "░";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.CameraTag);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 940);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(650, 40);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Roboto", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ready";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "[SCAN QR]";
            // 
            // ErrorTag
            // 
            this.ErrorTag.AutoSize = true;
            this.ErrorTag.BackColor = System.Drawing.Color.Red;
            this.ErrorTag.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTag.ForeColor = System.Drawing.Color.White;
            this.ErrorTag.Location = new System.Drawing.Point(268, 767);
            this.ErrorTag.Name = "ErrorTag";
            this.ErrorTag.Size = new System.Drawing.Size(95, 24);
            this.ErrorTag.TabIndex = 6;
            this.ErrorTag.Text = "[ERROR]";
            // 
            // MainRefresh
            // 
            this.MainRefresh.Interval = 200;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 73);
            this.panel1.TabIndex = 7;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Login_MouseUp);
            // 
            // Scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 980);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ErrorTag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Imager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scan";
            this.Text = "Scan";
            this.Load += new System.EventHandler(this.Scan_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Login_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.Imager)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Imager;
        private System.Windows.Forms.Timer Clock;
        private System.Windows.Forms.Label CameraTag;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ErrorTag;
        private System.Windows.Forms.Timer MainRefresh;
        private System.Windows.Forms.Panel panel1;
    }
}