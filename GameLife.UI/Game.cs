using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameLife.UI
{
    public class Game
    {
        GameOptions options;

        IView view;
        ISaveManager saveManager = new SaveManager();
        ILogManager logManager = new LogManager();

        public Map map;
        private GameStatus gameStatus;

        List<string> storyHash = new List<string>();
        public Game(GameOptions options, IView view, ISaveManager saveManager, ILogManager logManager)
        {
            this.options = options;
            this.view = view;
            this.saveManager = saveManager;
            this.logManager = logManager;
            map = new Map(options.Height, options.Width);
        }

        #region public
        public void Pause()
        {
            if (gameStatus == GameStatus.Paused)
            {
                gameStatus = GameStatus.Play;
            }
            else if (gameStatus == GameStatus.Play)
            {
                gameStatus = GameStatus.Paused;
            }
        }

        public async void Start()
        {
            InitGame();
            await Loop();
            GameStop();
        }

        public void Stop()
        {
            gameStatus = GameStatus.Stop;
        }
        #endregion
        public GameStatus GetStatus()
        {
            return gameStatus;
        }
        private Task Loop()
        {
            return Task.Run(() =>
            {
                while (!IsGameOver())
                {
                    while (gameStatus == GameStatus.Paused)
                    {
                        Task.Delay(100);
                    }
                    Update();
                    Render();
                    Thread.Sleep(1000 / options.FRAME);
                }
            });

        }

        private void InitGame()
        {
            map.generation = 0;
            gameStatus = GameStatus.Play;
            storyHash.Clear();
            StartLog();
        }

        private void Render()
        {
            view.Render(map);
        }

        private void Update()
        {
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    int neiborsCount = map.GetNeighborsCount(i, j);

                    if (map[i, j] != 0 && (neiborsCount < 2 || neiborsCount > 3))
                    {
                        map[i, j] = 0;
                    }
                    else if (map[i, j] == 0 && neiborsCount == 3)
                    {
                        map[i, j] = 1;
                    }
                    else
                        map[i, j] = map[i, j];
                }
            }
            map.Swap();
            map.generation += 1;
        }

        private bool IsGameOver()
        {
            bool result = false;
            if (gameStatus == GameStatus.Stop) return true;
            int died = 0;
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    if (map[i, j] == 0) died++;
                }
            }
            if (died == 0) return true;
            if (isRepeat()) return true;
            return result;
        }

        private void GameStop()
        {
            gameStatus = GameStatus.Stop;
            SaveLog();
        }

        private bool isRepeat()
        {
            string hash = map._current.GetHash();

            foreach (var item in storyHash)
            {
                if (item == hash)
                    return true;
            }

            if (storyHash.Count == 1000)
                storyHash.RemoveAt(0);
            storyHash.Add(hash);

            return false;
        }

        #region map
        public void GenerateMap(int DensityPercent)
        {
            Random rnd = new Random();
            for (int y = 0; y < map.Rows; y++)
            {
                for (int x = 0; x < map.Columns; x++)
                {
                    map._current[y, x] = (rnd.Next(100) <= DensityPercent) ? (byte)1 : (byte)0;
                }
            }
            Render();
        }

        public void ClearMap()
        {
            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    map[i, j] = 0;
                }
            }
            map.Swap();
            Render();
        }
        #endregion

        #region SaveManager
        public List<GameSaveDTO> GetGameSaves()
        {
            return saveManager.GetGameSaves();
        }

        public void CreateGameSave()
        {
            saveManager.CreateGameSave(map);
        }

        public void DeleteGameSave(int id)
        {
            saveManager.DeleteGameSave(id);
        }

        public void LoadGameSave(int id)
        {
            map = saveManager.LoadGameSave(id);
            Render();
        }

        public void LoadRandomGameSave()
        {
            map = saveManager.LoadRandomGameSave();
            Render();
        }
        #endregion

        #region LogManager
        public List<LogDTO> GetLogs()
        {
            return logManager.GetLogs();
        }

        private void StartLog()
        {
            logManager.StartLog(map);
        }

        private void SaveLog()
        {
            logManager.SaveLog(map);
        }
        #endregion

    }
}
