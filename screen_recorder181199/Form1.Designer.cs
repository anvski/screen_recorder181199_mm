
namespace screen_recorder181199
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
            this.components = new System.ComponentModel.Container();
            this.recBtn = new System.Windows.Forms.Button();
            this.slctFolder = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.lwSources = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.timerRec = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.pbScreenRec = new System.Windows.Forms.PictureBox();
            this.lbCodec = new System.Windows.Forms.ListBox();
            this.pbAudio = new System.Windows.Forms.ProgressBar();
            this.lbFormats = new System.Windows.Forms.ListBox();
            this.lbFramerate = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenRec)).BeginInit();
            this.SuspendLayout();
            // 
            // recBtn
            // 
            this.recBtn.Location = new System.Drawing.Point(88, 53);
            this.recBtn.Name = "recBtn";
            this.recBtn.Size = new System.Drawing.Size(75, 23);
            this.recBtn.TabIndex = 0;
            this.recBtn.Text = "Record";
            this.recBtn.UseVisualStyleBackColor = true;
            this.recBtn.Click += new System.EventHandler(this.recBtn_Click);
            // 
            // slctFolder
            // 
            this.slctFolder.Location = new System.Drawing.Point(713, 53);
            this.slctFolder.Name = "slctFolder";
            this.slctFolder.Size = new System.Drawing.Size(75, 23);
            this.slctFolder.TabIndex = 1;
            this.slctFolder.Text = "Select folder";
            this.slctFolder.UseVisualStyleBackColor = true;
            this.slctFolder.Click += new System.EventHandler(this.slctFolder_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(88, 121);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // lwSources
            // 
            this.lwSources.HideSelection = false;
            this.lwSources.Location = new System.Drawing.Point(88, 190);
            this.lwSources.MultiSelect = false;
            this.lwSources.Name = "lwSources";
            this.lwSources.Size = new System.Drawing.Size(180, 187);
            this.lwSources.TabIndex = 3;
            this.lwSources.UseCompatibleStateImageBehavior = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(109, 161);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(138, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh sources";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_click);
            // 
            // timerRec
            // 
            this.timerRec.Interval = 10;
            this.timerRec.Tick += new System.EventHandler(this.timerRec_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Maroon;
            this.lblTime.Location = new System.Drawing.Point(356, 121);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(96, 25);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "00:00:00";
            // 
            // pbScreenRec
            // 
            this.pbScreenRec.Location = new System.Drawing.Point(361, 157);
            this.pbScreenRec.Name = "pbScreenRec";
            this.pbScreenRec.Size = new System.Drawing.Size(427, 317);
            this.pbScreenRec.TabIndex = 6;
            this.pbScreenRec.TabStop = false;
            // 
            // lbCodec
            // 
            this.lbCodec.FormattingEnabled = true;
            this.lbCodec.Items.AddRange(new object[] {
            "MPEG",
            "H264"});
            this.lbCodec.Location = new System.Drawing.Point(361, 20);
            this.lbCodec.Name = "lbCodec";
            this.lbCodec.Size = new System.Drawing.Size(120, 95);
            this.lbCodec.TabIndex = 7;
            this.lbCodec.SelectedIndexChanged += new System.EventHandler(this.lbCodec_SelectedIndexChanged);
            // 
            // pbAudio
            // 
            this.pbAudio.Location = new System.Drawing.Point(12, 480);
            this.pbAudio.Name = "pbAudio";
            this.pbAudio.Size = new System.Drawing.Size(776, 23);
            this.pbAudio.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAudio.TabIndex = 8;
            // 
            // lbFormats
            // 
            this.lbFormats.FormattingEnabled = true;
            this.lbFormats.Location = new System.Drawing.Point(539, 20);
            this.lbFormats.Name = "lbFormats";
            this.lbFormats.Size = new System.Drawing.Size(120, 95);
            this.lbFormats.TabIndex = 9;
            this.lbFormats.SelectedIndexChanged += new System.EventHandler(this.lbFormats_SelectedIndexChanged);
            // 
            // lbFramerate
            // 
            this.lbFramerate.FormattingEnabled = true;
            this.lbFramerate.Location = new System.Drawing.Point(240, 49);
            this.lbFramerate.Name = "lbFramerate";
            this.lbFramerate.Size = new System.Drawing.Size(69, 95);
            this.lbFramerate.TabIndex = 10;
            this.lbFramerate.SelectedIndexChanged += new System.EventHandler(this.lbFramerate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Framerate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Video Codec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(536, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Output format";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 515);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbFramerate);
            this.Controls.Add(this.lbFormats);
            this.Controls.Add(this.pbAudio);
            this.Controls.Add(this.lbCodec);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lwSources);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.slctFolder);
            this.Controls.Add(this.recBtn);
            this.Controls.Add(this.pbScreenRec);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenRec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button recBtn;
        private System.Windows.Forms.Button slctFolder;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.ListView lwSources;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timerRec;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pbScreenRec;
        private System.Windows.Forms.ListBox lbCodec;
        private System.Windows.Forms.ProgressBar pbAudio;
        private System.Windows.Forms.ListBox lbFormats;
        private System.Windows.Forms.ListBox lbFramerate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

