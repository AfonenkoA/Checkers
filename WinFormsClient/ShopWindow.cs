using WinFormsClient.Control.Shop;
using WinFormsClient.Model;
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
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }

    public void BuyAnimation() => InvokeAction(OnBuyAnimation);
    public void BuyCheckersSkin() => InvokeAction(OnBuyCheckersSkin);
    public void BuyLootBox() => InvokeAction(OnBuyLootBox);

    public event Action OnBackToMenu;
    public event Action OnReloadShop;
    public event Action OnBuyAnimation;
    public event Action OnBuyCheckersSkin;
    public event Action OnBuyLootBox;
    public int CheckersSkinId { get; set; }
    public int AnimationId { get; set; }
    public int LootBoxId { get; set; }

    public void SetShopInfo(Shop shop)
    {
        Animations.Controls.Clear();
        CheckersSkins.Controls.Clear();
        Lootboxes.Controls.Clear();
        foreach (var a in shop.Animations)
            Animations.Controls.Add(new SoldAnimationShowPanel(this,a));
        foreach (var skin in shop.Skins)
            CheckersSkins.Controls.Add(new SoldCheckersShowPanel(this, skin));
    }

    private static void InvokeAction(Action? action) => action?.Invoke();


}