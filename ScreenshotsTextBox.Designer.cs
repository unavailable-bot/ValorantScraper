namespace ValorantScraper
{
    partial class ScreenshotsTextBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotsTextBox));
            txtScreenshots = new TextBox();
            btnCopyTemp = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtScreenshots
            // 
            txtScreenshots.BackColor = Color.Honeydew;
            txtScreenshots.Location = new Point(12, 44);
            txtScreenshots.Name = "txtScreenshots";
            txtScreenshots.Size = new Size(328, 23);
            txtScreenshots.TabIndex = 0;
            // 
            // btnCopyTemp
            // 
            btnCopyTemp.BackColor = Color.MistyRose;
            btnCopyTemp.Location = new Point(126, 79);
            btnCopyTemp.Name = "btnCopyTemp";
            btnCopyTemp.Size = new Size(97, 30);
            btnCopyTemp.TabIndex = 1;
            btnCopyTemp.Text = "Copy Template";
            btnCopyTemp.UseVisualStyleBackColor = false;
            btnCopyTemp.Click += btnCopyTemp_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 26);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 2;
            label1.Text = "Screenshots link";
            // 
            // ScreenshotsTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(344, 121);
            Controls.Add(label1);
            Controls.Add(btnCopyTemp);
            Controls.Add(txtScreenshots);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ScreenshotsTextBox";
            Text = "Screenshots";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtScreenshots;
        private Button btnCopyTemp;
        private Label label1;
    }
}