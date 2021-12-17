using WinFormsClient.Model;
using WinFormsClient.Presentation.Views;

namespace WinFormsClient;

public partial class CollectionWindow : Form, ICollectionView
{
    private readonly ApplicationContext _context;
    public CollectionWindow(ApplicationContext context)

    {
        _context = context;
        InitializeComponent();
        ReturnButton.Click += (_, _) => InvokeAction(OnBackToMenu);
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }


    public void SetCollectionInfo(Collection collection)
    {
        foreach (var animation in collection.Animations)
            Aniamtions.Controls.Add(new SelectedItemShowPanel(this,animation));
        
        foreach (var skin in collection.Skins)
            CheckersSkins.Controls.Add(new SelectedItemShowPanel(this,skin));
    }

    public event Action? OnBackToMenu;
    public event Action? OnAnimationSelect;
    public event Action? OnCheckersSkinSelect;
    public int SelectedCheckersId { get; set; }
    public int SelectedAnimationsId { get; set; }
    private static void InvokeAction(Action? action) => action?.Invoke();
}