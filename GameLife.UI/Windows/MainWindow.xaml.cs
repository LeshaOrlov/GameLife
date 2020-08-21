using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameLife.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Game game;


        int sizeCell = 10;
        public MainWindow()
        {
            InitializeComponent();
            GameOptions options = new GameOptions
            {
                Height = Convert.ToInt32(GameArea.Height / sizeCell),
                Width = Convert.ToInt32(GameArea.Width / sizeCell)
            };
            game = new Game(options, this, new SaveManager(), new LogManager());

        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            GenerateMapBtn.IsEnabled = false;
            ClearMapBtn.IsEnabled = false;
            PauseGameBtn.IsEnabled = true;
            LoadMm.IsEnabled = false;
            LoadRandomMm.IsEnabled = false;
            SaveMm.IsEnabled = false;
            Border.IsEnabled = false;
            game.Start();
            Status.Content = game.GetStatus();
        }

        private void StopGame(object sender, RoutedEventArgs e)
        {
            game.Stop();
            GenerateMapBtn.IsEnabled = true;
            ClearMapBtn.IsEnabled = true;
            PauseGameBtn.IsEnabled = false;
            LoadMm.IsEnabled = true;
            LoadRandomMm.IsEnabled = true;
            SaveMm.IsEnabled = true;
            Border.IsEnabled = true;
            Status.Content = game.GetStatus();
        }

        private void PauseGame(object sender, RoutedEventArgs e)
        {
            game.Pause();
            GameStatus gs = game.GetStatus();
            Status.Content = gs;
            if (gs == GameStatus.Paused) SaveMm.IsEnabled = true;
            if (gs == GameStatus.Play) SaveMm.IsEnabled = false;
        }

        public void GenerateMap(object sender, RoutedEventArgs e)
        {
            var DensityPercent = Convert.ToInt32(DensityTxt.Text);
            game.GenerateMap(DensityPercent);
        }

        private void SaveGame(object sender, RoutedEventArgs e)
        {
            game.CreateGameSave();
        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            GameSaveWindow gsWin = new GameSaveWindow(game);
            gsWin.ShowDialog();
            Border.IsChecked = game.map.haveBorder;
        }

        private void LoadRandomGame(object sender, RoutedEventArgs e)
        {
            game.LoadRandomGameSave();
            Border.IsChecked = game.map.haveBorder; 
        }

        private void Hand_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);
                Point gameAreaPoint = GameArea.TransformToAncestor(this).Transform(new Point(0, 0));
                int CX = Convert.ToInt32(Math.Truncate((p.X - gameAreaPoint.X) / sizeCell));
                int CY = Convert.ToInt32(Math.Truncate((p.Y - gameAreaPoint.Y) / sizeCell));
                game.map._current[CY, CX] = 1;
                DrawCell(CY, CX, sizeCell);
            }
        }

        public void InitCanvas()
        {
            GameArea.Children.Clear();
            for (int i = 0; i < game.map.Rows; i++)
            {
                for (int j = 0; j < game.map.Columns; j++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = sizeCell,
                        Height = sizeCell,
                        Fill = Brushes.Green
                    };
                    Canvas.SetTop(rect, i * sizeCell);
                    Canvas.SetLeft(rect, j * sizeCell);
                    GameArea.Children.Add(rect);
                }
            }
        }

        public void Render(Map map)
        {
            Action action = () =>
            {
                FPS.Content = 1;
                Step.Content = map.generation;
                GameArea.Children.Clear();
                for (int i = 0; i < map.Rows; i++)
                {
                    for (int j = 0; j < map.Columns; j++)
                    {
                        if (map[i, j] != 0)
                        {
                            DrawCell(i, j, sizeCell);
                        }
                    }
                }
            };

            this.Dispatcher.Invoke(action);
        }

        private void DrawCell(int i, int j, int sizeCell)
        {
            Rectangle rect = new Rectangle
            {
                Width = sizeCell,
                Height = sizeCell,
                Fill = Brushes.Green
            };
            Canvas.SetTop(rect, i * sizeCell);
            Canvas.SetLeft(rect, j * sizeCell);
            GameArea.Children.Add(rect);
        }

        public void GameSaveWin(object sender, RoutedEventArgs e)
        {
            GameSaveWindow gsWin = new GameSaveWindow(game);
            gsWin.Show();
        }

        public void AboutProgram(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        public void ShowLogs(object sender, RoutedEventArgs e)
        {
            LogsWindow logsWindow = new LogsWindow(game);
            logsWindow.Show();
        }

        public void ExitProgram(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClearMap(object sender, RoutedEventArgs e)
        {
            GameArea.Children.Clear();
            game.ClearMap();
        }

        private void Border_Checked(object sender, RoutedEventArgs e)
        {
            game.map.haveBorder = true;
        }

        private void Border_Unchecked(object sender, RoutedEventArgs e)
        {
            game.map.haveBorder = false;
        }
        
    }
}

