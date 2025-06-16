namespace ValorantScraper
{
    public partial class AgentsResult : Form, IImageDisplay
    {
        public FlowLayoutPanel FlowLayoutPanelImages => flowLayoutPanelAgents;

        public void AddControl(Control control)
        {
            this.Controls.Add(control);
        }

        public AgentsResult()
        {
            InitializeComponent();
        }
    }
}
