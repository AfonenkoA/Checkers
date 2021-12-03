using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;

namespace WinFormsClient;

public partial class ShopWindow : Form
{

    private readonly MainMenuWindow _menuWindow;

    public ShopWindow(List<Control> l1,List<Control> l2,List<Control> l3)
    {
        flowLayoutPanel1.Controls.AddRange(l1.ToArray());
        flowLayoutPanel2.Controls.AddRange(l2.ToArray());
        flowLayoutPanel3.Controls.AddRange(l3.ToArray());
        InitializeComponent();
    }


    private void ReturnButton_Click(object sender, EventArgs e)
    {
        Hide();
        _menuWindow.Show();
    }

    private void ShopWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _menuWindow.Close();
    }
}