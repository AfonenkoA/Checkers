using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient
{
    public partial class ItemShowPanel : UserControl
    {
        
        private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
        private readonly DetailedItem _item;
        public ItemShowPanel(DetailedItem item)
        {
            _item = item;
            InitializeComponent();
        }


        private  void ItemShowPanel_Load(object sender, EventArgs e)
        {
            
            TitleLabel.Text = _item.Name;
            DescriptionLabel.Text = _item.Detail;
        }
    }
}