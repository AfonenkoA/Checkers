using Common.Entity;
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

    public void SetCollectionInfo(IEnumerable<Animation> animations,
        IEnumerable<CheckersSkin> skins)
    {
        foreach (var animation in animations)
            flowLayoutPanel1.Controls.Add(new ItemShowPanel(animation));
        
        foreach (var skin in skins)
            flowLayoutPanel2.Controls.Add(new ItemShowPanel(skin));
    }

    public event Action BackToMenu;
    private void Invoke(Action action)
    {
        if (action != null) action();
    }
}