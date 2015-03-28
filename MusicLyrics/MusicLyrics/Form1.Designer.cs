namespace MusicLyrics
{
    partial class Music
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
            this.New_Band = new System.Windows.Forms.Button();
            this.Exisiting_Band = new System.Windows.Forms.Button();
            this.Enter_Band_Name = new System.Windows.Forms.TextBox();
            this.Enter_Band_Choice = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BandList = new System.Windows.Forms.ListView();
            this.Bands = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Display_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // New_Band
            // 
            this.New_Band.Location = new System.Drawing.Point(71, 344);
            this.New_Band.Name = "New_Band";
            this.New_Band.Size = new System.Drawing.Size(100, 50);
            this.New_Band.TabIndex = 0;
            this.New_Band.Text = "New Band";
            this.New_Band.UseVisualStyleBackColor = true;
            this.New_Band.Click += new System.EventHandler(this.New_Band_Click);
            // 
            // Exisiting_Band
            // 
            this.Exisiting_Band.Enabled = false;
            this.Exisiting_Band.Location = new System.Drawing.Point(305, 344);
            this.Exisiting_Band.Name = "Exisiting_Band";
            this.Exisiting_Band.Size = new System.Drawing.Size(100, 50);
            this.Exisiting_Band.TabIndex = 1;
            this.Exisiting_Band.Text = "Exisiting Band";
            this.Exisiting_Band.UseVisualStyleBackColor = true;
            this.Exisiting_Band.Click += new System.EventHandler(this.Exisiting_Band_Click);
            // 
            // Enter_Band_Name
            // 
            this.Enter_Band_Name.BackColor = System.Drawing.SystemColors.Window;
            this.Enter_Band_Name.Enabled = false;
            this.Enter_Band_Name.Location = new System.Drawing.Point(71, 356);
            this.Enter_Band_Name.Name = "Enter_Band_Name";
            this.Enter_Band_Name.Size = new System.Drawing.Size(163, 26);
            this.Enter_Band_Name.TabIndex = 2;
            this.Enter_Band_Name.Visible = false;
            // 
            // Enter_Band_Choice
            // 
            this.Enter_Band_Choice.Enabled = false;
            this.Enter_Band_Choice.Location = new System.Drawing.Point(305, 344);
            this.Enter_Band_Choice.Name = "Enter_Band_Choice";
            this.Enter_Band_Choice.Size = new System.Drawing.Size(100, 50);
            this.Enter_Band_Choice.TabIndex = 3;
            this.Enter_Band_Choice.Text = "Enter";
            this.Enter_Band_Choice.UseVisualStyleBackColor = true;
            this.Enter_Band_Choice.Visible = false;
            this.Enter_Band_Choice.Click += new System.EventHandler(this.Enter_Band_Choice_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MusicLyrics.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(82, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // BandList
            // 
            this.BandList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Bands});
            this.BandList.Enabled = false;
            this.BandList.Location = new System.Drawing.Point(82, 2);
            this.BandList.Name = "BandList";
            this.BandList.Size = new System.Drawing.Size(300, 300);
            this.BandList.TabIndex = 5;
            this.BandList.UseCompatibleStateImageBehavior = false;
            this.BandList.Visible = false;
            // 
            // Display_Button
            // 
            this.Display_Button.Enabled = false;
            this.Display_Button.Location = new System.Drawing.Point(305, 344);
            this.Display_Button.Name = "Display_Button";
            this.Display_Button.Size = new System.Drawing.Size(100, 50);
            this.Display_Button.TabIndex = 6;
            this.Display_Button.Text = "Display";
            this.Display_Button.UseVisualStyleBackColor = true;
            this.Display_Button.Visible = false;
            this.Display_Button.Click += new System.EventHandler(this.Display_Button_Click);
            // 
            // Music
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 444);
            this.Controls.Add(this.Display_Button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Enter_Band_Choice);
            this.Controls.Add(this.Enter_Band_Name);
            this.Controls.Add(this.Exisiting_Band);
            this.Controls.Add(this.New_Band);
            this.Controls.Add(this.BandList);
            this.Name = "Music";
            this.Text = "Music Lyrics Analyser";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button New_Band;
        private System.Windows.Forms.Button Exisiting_Band;
        private System.Windows.Forms.TextBox Enter_Band_Name;
        private System.Windows.Forms.Button Enter_Band_Choice;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView BandList;
        public System.Windows.Forms.ColumnHeader Bands;
        private System.Windows.Forms.Button Display_Button;
    }
}

