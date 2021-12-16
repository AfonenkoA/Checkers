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
    public partial class SoldItemShowPanel : UserControl
    {
        public SoldItemShowPanel(VisualSoldItem item)
        {
            
            InitializeComponent();
            TitleLabel.Text = item.Name;
            DescriptionLabel.Text = item.Detail;
            PriceLabel.Text = item.Price.ToString();
            pictureBox1.Image = item.Image;

        }
    }
}
