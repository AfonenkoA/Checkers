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

    public void BuyAnimation() => InvokeAction(OnBuyAnimation);
    public void BuyCheckersSkin() => InvokeAction(OnBuyCheckersSkin);
    public void BuyLootBox() => InvokeAction(OnBuyLootBox);


    
    public void SetShopInfo(Credential credential,IEnumerable<VisualAnimation>animations,
        IEnumerable<VisualCheckersSkin> checkersSkin,
        IEnumerable<VisualLootBox> lootBoxes)
    {
        Aniamtions.Controls.Clear();
        CheckersSkins.Controls.Clear();
        //  foreach (var animation in animations)
            Animations.Controls.Add(new SoldAnimationShowPanel(this,animation));
        // foreach (var lootBox in lootBoxes)
        //  Lootboxes.Controls.Add(new SoldAnimationShowPanel(this,lootBox));
        foreach (var skin in checkersSkin)
            CheckersSkins.Controls.Add(new SoldCheckersShowPanel(this,skin));
    }

    public event Action OnBackToMenu;
    public event Action ReloadShop;
    public event Action OnBuyAnimation;
    public event Action OnBuyCheckersSkin;
    public event Action OnBuyLootBox;
    public int CheckersSkinId { get; set; }
    public int AnimationId { get; set; }
    public int LootBoxId { get; set; }

    private static void InvokeAction(Action? action) => action?.Invoke();


}