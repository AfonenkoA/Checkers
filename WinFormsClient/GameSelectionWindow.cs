﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsClient
{
    public partial class GameSelectionWindow : Form
    {
        private readonly MainMenuWindow _menuWindow;
        public GameSelectionWindow(MainMenuWindow window)
        {
            _menuWindow = window ;
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Hide();
            _menuWindow.Show();

        }

        private void GameSelectionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _menuWindow.Close();
        }
    }
}
