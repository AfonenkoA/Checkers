using WinFormsClient.Model;
using WinFormsClient.Presentation.Views;
using static System.Drawing.Graphics;

namespace WinFormsClient;

public sealed partial class GameWindow : Form, IGameView
{
    private const int Cells = 10;
    private readonly Graphics _graphics;
    private readonly Bitmap _bitmap;

    private void DrawBoard()
    {
        var w = _bitmap.Width / Cells;
        var h = _bitmap.Height / Cells;
        _graphics.Clear(Color.Bisque);
        var isBlack = false;
        var pen = new Pen(Color.Black);
        Brush brush = new SolidBrush(Color.SaddleBrown);
        for (var i = 0; i < Cells; i++)
        {
            for (var j = 0; j < Cells; j++)
            {
                if (!isBlack)
                {
                    isBlack = true;
                    continue;
                }
                var rectangle = new Rectangle(i * w, j * h, w, h);
                _graphics.DrawRectangle(pen, rectangle);
                _graphics.FillRectangle(brush, rectangle);
                isBlack = false;
            }
            isBlack = !isBlack;
        }
    }

    public new void Show()
    {
        _context.MainForm = this;
        base.Show();
    }

    private readonly ApplicationContext _context;

    public GameWindow(ApplicationContext context)

    {
        _context = context;
        InitializeComponent();
        _bitmap = new Bitmap(Board.Width, Board.Height);
        _graphics = FromImage(_bitmap);
    }

    private void GameWindow_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

    public void SetGameInfo(Self self, User enemy)
    {
        DrawBoard();
        Board.Image = _bitmap;
        SelfNick.Text = self.Nick;
        SelfPicture.Image = self.Image;
        SelfSocialCredit.Text = self.SocialCredit.ToString();
        EnemyNick.Text = enemy.Nick;
        EnemyPicture.Image = enemy.Image;
        EnemySocialCredit.Text = enemy.SocialCredit.ToString();
    }
}