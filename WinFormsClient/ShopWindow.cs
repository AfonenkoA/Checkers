using Common.Entity;
using WinFormsClient.Control.Shop;
using WinFormsClient.Model.Item;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class ShopWindow : Form, IShopView
{
    private readonly ApplicationContext _context;
    public ShopWindow(ApplicationContext context)

    {
        _context = context;
        InitializeComponent();
        ReturnButton.Click+= (sender, args) => Invoke(OnBackToMenu);
        ReloadShopButton.Click += (sender, args) => Invoke(ReloadShop);
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }



    public event Action OnBackToMenu;
    public event Action ReloadShop;
    public void SetShopInfo(Credential credential,IEnumerable<VisualAnimation>animations,
        IEnumerable<VisualCheckersSkin> checkersSkin,
        IEnumerable<VisualLootBox> lootBoxes)
    {
        foreach (var animation in animations)
            Animations.Controls.Add(new SoldAnimationShowPanel(animation,credential));
        foreach (var lootBox in lootBoxes)
            Lootboxes.Controls.Add(new SoldAnimationShowPanel(lootBox,credential));
        foreach (var skin in checkersSkin)
            CheckersSkins.Controls.Add(new SoldCheckersShowPanel(skin,credential));
    }



    private void Invoke(Action action)
    {
        if (action != null) action();
    }

   
}