using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameLife.UI
{
    /// <summary>
    /// Логика взаимодействия для GameSaveWindow.xaml
    /// </summary>
    public partial class GameSaveWindow : Window
    {
        Game game;
        public GameSaveWindow(Game game)
        {
            InitializeComponent();
            this.game = game;
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }
        
        public void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        public void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameSavesGrid.SelectedItems != null)
            {
                var list = gameSavesGrid.SelectedItems.Cast<GameSaveDTO>();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        game.LoadGameSave(item.ID);
                    }
                }
            }
        }

        public void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameSavesGrid.SelectedItems != null)
            {
                var list = gameSavesGrid.SelectedItems.Cast<GameSaveDTO>();
                foreach(var item in list)
                {
                    game.DeleteGameSave(item.ID);
                }
            }
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            gameSavesGrid.ItemsSource = game.GetGameSaves();
        }
    }
}
