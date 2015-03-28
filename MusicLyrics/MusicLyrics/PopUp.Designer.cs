namespace MusicLyrics
{
    partial class PopUp
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
            this.chartArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartArea)).BeginInit();
            this.SuspendLayout();
            // 
            // chartArea
            // 
            this.chartArea.BackColor = System.Drawing.Color.White;
            this.chartArea.Location = new System.Drawing.Point(13, 13);
            this.chartArea.Name = "chartArea";
            this.chartArea.Size = new System.Drawing.Size(450, 400);
            this.chartArea.TabIndex = 0;
            this.chartArea.TabStop = false;
            this.chartArea.MouseHover += new System.EventHandler(this.chartArea_Hover);
            this.ResizeEnd += new System.EventHandler(this.setTrue);
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 444);
            this.Controls.Add(this.chartArea);
            this.Name = "PopUp";
            this.Text = "PopUp";
            ((System.ComponentModel.ISupportInitialize)(this.chartArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox chartArea;
    }
}