using System;
using System.Windows.Forms;
using static System.Array;

namespace OldWinFormsClient;

public partial class AchievementsWindow : Form
{
    private readonly MainMenuWindow _mainMenuWindow;
    private int[] _achievements;
    public int[] Achievements
    {
        get => _achievements;
        set
        {
            _achievements = value;
            Refresh();
            Show();
        }
    }

    public AchievementsWindow(MainMenuWindow window)
    {
        _mainMenuWindow = window;
        _achievements = Empty<int>();
        InitializeComponent();
    }

    public void UpdateElements()
    {
        AchievementsPanel.Controls.Clear();
        foreach (int id in _achievements)
            AchievementsPanel.Controls.Add(
                new ItemShowPanel($"Achievement{id}Pic",
                    $"Achievement{id}Title",
                    $"Achievement{id}Desc"));
    }

    public override void Refresh()
    {
        UpdateElements();
        base.Refresh();
    }

    private void BackToMenuButton_Click(object sender, EventArgs e)
    {
        Hide();
        _mainMenuWindow.Show();
    }

    private void AchievementsWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        _mainMenuWindow.Close();
    }
}