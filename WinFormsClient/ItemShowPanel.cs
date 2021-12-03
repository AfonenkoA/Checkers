using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using static System.IO.File;

namespace WinFormsClient
{
    public partial class ItemShowPanel : UserControl
    {
        private static readonly IAsyncResourceService ResourceService = new AsyncResourceWebApi();
        private static readonly int FileIndex = 100;
        private readonly DetailedItem _item;
        public ItemShowPanel(DetailedItem item)
        {
            _item = item;
            InitializeComponent();
        }


        private async void ItemShowPanel_Load(object sender, EventArgs e)
        {
            var (_, bytes) = await ResourceService.TryGetFile(_item.Resource.Id).ConfigureAwait(false);
            var file = $"{FileIndex}." + _item.Resource.Extension;
            await WriteAllBytesAsync(file, bytes);
            pictureBox1.Image = Image.FromFile(file);
            TitleLabel.Text = _item.Name;
            DescriptionLabel.Text = _item.Detail;
        }
    }
}
