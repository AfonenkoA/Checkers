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



    public event Action OnBackToMenu;
    public void SetShopInfo(IEnumerable<VisualAnimation> animations,
        IEnumerable<VisualCheckersSkin> checkersSkin,
        IEnumerable<VisualLootBox> lootBoxes)
    {
        foreach (var animation in animations)
            Animations.Controls.Add(new ItemShowPanel(animation));
        foreach (var lootBox in lootBoxes)
            LootBoxes.Controls.Add(new ItemShowPanel(lootBox));
        foreach (var skin in checkersSkin)
            CheckersSkins.Controls.Add(new ItemShowPanel(skin));
    }



    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}