namespace WindowsFormsApp1
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
            this.buttonShowLiveLabel = new System.Windows.Forms.Button();
            this.labelLiveStatus = new LiveLabel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonShowLiveLabel
            // 
            this.buttonShowLiveLabel.Location = new System.Drawing.Point(13, 13);
            this.buttonShowLiveLabel.Name = "buttonShowLiveLabel";
            this.buttonShowLiveLabel.Size = new System.Drawing.Size(642, 79);
            this.buttonShowLiveLabel.TabIndex = 0;
            this.buttonShowLiveLabel.Text = "&Show a label and have it announced by a screen reader";
            this.buttonShowLiveLabel.UseVisualStyleBackColor = true;
            this.buttonShowLiveLabel.Click += new System.EventHandler(this.buttonShowLiveLabel_Click);
            // 
            // labelLiveStatus
            // 
            this.labelLiveStatus.AutoSize = true;
            this.labelLiveStatus.Location = new System.Drawing.Point(13, 116);
            this.labelLiveStatus.Name = "labelLiveStatus";
            this.labelLiveStatus.Size = new System.Drawing.Size(0, 25);
            this.labelLiveStatus.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(580, 239);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(176, 82);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 333);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelLiveStatus);
            this.Controls.Add(this.buttonShowLiveLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Demo of the WinForms LiveLabel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShowLiveLabel;
        private LiveLabel labelLiveStatus;
        private System.Windows.Forms.Button buttonClose;
    }
}

