using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValorantScraper
{
    public partial class Form2 : Form, IImageDisplay
    {
        public Form2()
        {
            InitializeComponent();
        }

        public FlowLayoutPanel FlowLayoutPanelImages => flowLayoutPanelImages;

        public void AddControl(Control control)
        {
            this.Controls.Add(control);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
