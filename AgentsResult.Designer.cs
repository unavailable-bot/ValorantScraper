namespace ValorantScraper
{
    partial class AgentsResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentsResult));
            flowLayoutPanelAgents = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowLayoutPanelAgents
            // 
            flowLayoutPanelAgents.AutoScroll = true;
            flowLayoutPanelAgents.Dock = DockStyle.Fill;
            flowLayoutPanelAgents.Location = new Point(0, 0);
            flowLayoutPanelAgents.Name = "flowLayoutPanelAgents";
            flowLayoutPanelAgents.Size = new Size(800, 450);
            flowLayoutPanelAgents.TabIndex = 0;
            // 
            // AgentsResult
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(flowLayoutPanelAgents);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AgentsResult";
            Text = "AgentsResult";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelAgents;
    }
}