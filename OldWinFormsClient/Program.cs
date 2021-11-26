using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Checkers.Client;

namespace OldWinFormsClient;

public sealed class ItemPanel : FlowLayoutPanel
{
    private sealed class ItemLabel : Label
    {
        public ItemLabel(string text)
        {
            AutoSize = true;
            Text = text;
            BorderStyle = BorderStyle.FixedSingle;
        }
    }

    private static readonly ResourceManager Manager = Properties.Resources.ResourceManager;
    public ItemPanel(int id)
    {
        WrapContents = false;
        AutoScroll = false;
        AutoSize = true;
        FlowDirection = FlowDirection.TopDown;
        Controls.Add(new PictureBox
        {
            Height = 100,
            Width = 100,
            Image = Manager.GetObject($"Item{id}Pic") as Image,
            SizeMode = PictureBoxSizeMode.Zoom,
        });
        Controls.Add(new ItemLabel(Manager.GetString($"Item{id}Title") ?? string.Empty));
        Controls.Add(new ItemLabel(Manager.GetString($"Item{id}Desc") ?? string.Empty));
    }
}


internal static class Common
{
    internal static GameClient RegisterClient(string login, string password)
    {
        return Client = new GameClient(login, password);
    }
    public static GameClient? Client { get; private set; }
}
static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new LoginWindow());
    }
}