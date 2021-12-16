using Common.Entity;
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
        ReturnButton.Click += (sender, args) => Invoke(BackToMenu);
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }

    public void SetCollectionInfo(IEnumerable<VisualAnimation> animations,
        IEnumerable<VisualCheckersSkin> skins)
    {
        foreach (var animation in animations)
            Aniamtions.Controls.Add(new SelectedItemShowPanel(animation));
        
        foreach (var skin in skins)
            CheckersSkins.Controls.Add(new SelectedItemShowPanel(skin));
    }

    public event Action BackToMenu;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}