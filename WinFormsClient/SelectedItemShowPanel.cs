using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.Model;

namespace WinFormsClient
{
    public partial class SelectedItemShowPanel : UserControl
    {
        public SelectedItemShowPanel(VisualDetailedItem item)
        {
            InitializeComponent();
            TitleLabel.Text = item.Name;
            DescriptionLabel.Text = item.Detail;
            pictureBox1.Image = item.Image;
        }
    }
}
