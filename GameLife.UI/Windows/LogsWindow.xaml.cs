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
    /// Логика взаимодействия для LogsWindow.xaml
    /// </summary>
    public partial class LogsWindow : Window
    {
        Game game;
        public LogsWindow(Game game)
        {
            InitializeComponent();
            this.game = game;
    }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            LogsGrid.ItemsSource = game.GetLogs();
        }

    }
}
