using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.Transmission;
using static System.Array;

namespace WinFormsClient
{
    public partial class FriendsWindow : Form
    {
        private readonly Form _parentForm;
        private string[] _friends;
        public string[] Friends
        {
            get => _friends;
            set
            {
                _friends = value;
                Refresh();
                Show();
            }
        }

        public FriendsWindow(Form form)
        {
            _parentForm = form;
            _friends = Empty<string>();
            InitializeComponent();
        }

        public async void UpdateElements()
        {
            FriendsPanel.Controls.Clear();
            List<Task<UserGetResponse>> tasks = new(_friends.Length);
            foreach (string friend in _friends)
                tasks.Add(Common.Client.GetUserAsync(friend));
            foreach (var task in tasks)
            {
                UserGetResponse response = await task;
                Button button = new()
                {
                    Text = response.Info.Nick,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                };
                button.Click += (o, a) =>
                {
                    new ProfileWindow(response.Info);
                };
                FriendsPanel.Controls.Add(button);
            }

        }

        public override void Refresh()
        {
            UpdateElements();
            base.Refresh();
        }

        private void FriendsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Close();
        }

        private void BackToMenuButton_Click(object sender, EventArgs e)
        {
            Hide();
            _parentForm.Show();
        }
    }
}
