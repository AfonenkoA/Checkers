using System;
using System.Windows.Forms;
using Checkers.Client;
using System.Resources;
using System.Drawing;

namespace WinFormsClient
{


    public class ItemPanel : FlowLayoutPanel
    {
        private class ItemLabel : Label
        {
            public ItemLabel(string text)
            {
                AutoSize = true;
                Text = text;
                BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private static readonly ResourceManager manager = Properties.Resources.ResourceManager;
        public ItemPanel(int id)
        {
            WrapContents = false;
            AutoScroll = false;
            AutoSize = true;
            FlowDirection = FlowDirection.TopDown;
            Controls.Add(new PictureBox()
            {
                Height = 100,
                Width = 100,
                Image = (Image)manager.GetObject($"Item{id}Pic"),
                SizeMode = PictureBoxSizeMode.Zoom,
            });
            Controls.Add(new ItemLabel(manager.GetString($"Item{id}Title")));
            Controls.Add(new ItemLabel(manager.GetString($"Item{id}Desc") ));
        }
    }


    public static class Common
    {
        public static GameClient RegisterClient(string login, string password)
        {
            return Client = new GameClient(login, password);
        }
        public static GameClient Client { get; private set; }
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
}
