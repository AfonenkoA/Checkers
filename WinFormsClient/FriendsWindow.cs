﻿namespace WinFormsClient;

public partial class FriendsWindow : Form
{
    private readonly MainMenuWindow _menuWindow;
    public FriendsWindow(MainMenuWindow window)
    {
        _menuWindow = window;
        InitializeComponent();
    }

    private void FriendsWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _menuWindow.Close();
    }

    private void ReturnButton_Click(object sender, EventArgs e)
    {
        Hide();
        _menuWindow.Show();
    }
}