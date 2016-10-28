namespace FFRC.Console
{
    partial class MainForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.comboWeek = new System.Windows.Forms.ComboBox();
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblTeam = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(84, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Fantasy Football Rankings Calculator";
            // 
            // comboWeek
            // 
            this.comboWeek.FormattingEnabled = true;
            this.comboWeek.Location = new System.Drawing.Point(72, 61);
            this.comboWeek.Name = "comboWeek";
            this.comboWeek.Size = new System.Drawing.Size(46, 21);
            this.comboWeek.TabIndex = 1;
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(27, 64);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 13);
            this.lblWeek.TabIndex = 2;
            this.lblWeek.Text = "Week:";
            // 
            // lblTeam
            // 
            this.lblTeam.AutoSize = true;
            this.lblTeam.Location = new System.Drawing.Point(27, 114);
            this.lblTeam.Name = "lblTeam";
            this.lblTeam.Size = new System.Drawing.Size(132, 13);
            this.lblTeam.TabIndex = 3;
            this.lblTeam.Text = "Team: (each in a new line)";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(30, 141);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(397, 219);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(172, 373);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(119, 23);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "Show Rankings";
            this.btnShow.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 408);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblTeam);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.comboWeek);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fantasy Football Rankings Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox comboWeek;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.Label lblTeam;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnShow;
    }
}