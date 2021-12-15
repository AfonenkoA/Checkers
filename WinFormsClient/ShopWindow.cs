using Common.Entity;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class ShopWindow : Form, IShopView
{
    private readonly ApplicationContext _context;
    public ShopWindow(ApplicationContext context)

    {
        _context = context;
        InitializeComponent();
        ReturnButton.Click+= (sender, args) => Invoke(BackToMenu);
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }

    public void SetShopInfo(IEnumerable<Animation> animations,
        IEnumerable<LootBox> lootBoxes,
        IEnumerable<CheckersSkin> skins)
    {
        foreach (var animation in animations)
            flowLayoutPanel1.Controls.Add(new ItemShowPanel(animation));
        foreach (var lootBox in lootBoxes)
            flowLayoutPanel2.Controls.Add(new ItemShowPanel(lootBox));
        foreach (var skin in skins)
            flowLayoutPanel3.Controls.Add(new ItemShowPanel(skin));
    }

    public event Action BackToMenu;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}