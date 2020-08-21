using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public interface ISaveManager
    {
        List<GameSaveDTO> GetGameSaves();

        void CreateGameSave(Map map);

        void DeleteGameSave(int id);

        Map LoadGameSave(int id);

        Map LoadRandomGameSave();


    }
}
