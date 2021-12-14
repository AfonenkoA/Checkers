using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class ShopWindow : Form, IShopView
{
    private readonly ApplicationContext _context;
    public ShopWindow(ApplicationContext context)

    {
        _context = context;
        InitializeComponent();
}

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }

    public void SetShopInfo(List<Control> l1, List<Control> l2, List<Control> l3)
    {
        flowLayoutPanel1.Controls.AddRange(l1.ToArray());
        flowLayoutPanel2.Controls.AddRange(l2.ToArray());
        flowLayoutPanel3.Controls.AddRange(l3.ToArray());
    }
}