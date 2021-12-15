using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using WinFormsClient.Presentation.Common;

namespace WinFormsClient
{
    public partial class ItemShowPanel : UserControl
    {
        
        private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
        private readonly DetailedItem _item;
       

        public ItemShowPanel(DetailedItem item,Image img)
        {
            _item = item;
            InitializeComponent();
            TitleLabel.Text = _item.Name;
            DescriptionLabel.Text = _item.Detail;
            pictureBox1.Image = img;
        }


        
    }
}