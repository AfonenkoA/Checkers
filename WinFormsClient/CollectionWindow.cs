using System.Windows.Forms;
using static System.Array;

namespace WinFormsClient.Windows
{
    public partial class CollectionWindow : Form
    {
        private int[] _items;
        private Form _parentForm;

        public int[] Items
        {
            get => _items;
            set
            {
                _items = value;
                Refresh();
                Show();
            }
        }

        public CollectionWindow(MainMenuWindow mainMenuWindow)
        {
            this._parentForm = mainMenuWindow;
            _items = Empty<int>();
            InitializeComponent();
        }

        public void UpdateElements()
        {
            ItemsPanel.Controls.Clear();
            foreach (int item in _items)
                ItemsPanel.Controls.Add(new ItemShowPanel(
                    $"Item{item}Pic",
                    $"Item{item}Title",
                    $"Item{item}Desc"));
        }

        public override void Refresh()
        {
            UpdateElements();
            base.Refresh();
        }

        private void BackToMenuButton_Click(object sender, System.EventArgs e)
        {
            Hide();
            _parentForm.Show();
        }

        private void CollectionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Close();
        }
    }
}
