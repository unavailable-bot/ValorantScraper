namespace ValorantScraper
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            flowLayoutPanelImages = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowLayoutPanelImages
            // 
            flowLayoutPanelImages.AutoScroll = true;
            flowLayoutPanelImages.BackColor = SystemColors.GradientActiveCaption;
            flowLayoutPanelImages.Dock = DockStyle.Fill;
            flowLayoutPanelImages.Location = new Point(0, 0);
            flowLayoutPanelImages.Name = "flowLayoutPanelImages";
            flowLayoutPanelImages.Size = new Size(800, 450);
            flowLayoutPanelImages.TabIndex = 0;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanelImages);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            Text = "SkinsResult";
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        public FlowLayoutPanel flowLayoutPanelImages;
    }
}