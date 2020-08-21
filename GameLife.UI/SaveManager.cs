using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    class SaveManager : ISaveManager
    {
        EntityContext db = new EntityContext();
        public List<GameSaveDTO> GetGameSaves()
        {
            var result = db.GameSaves.Select(x => new GameSaveDTO
            {
                ID = x.ID,
                Date = x.Date
            }).ToList();
            return result;
        }

        public void CreateGameSave(Map map)
        {
            string hash = map._current.GetHash();

            if (db.GameSaves.Any(x => x.Hash == hash))
                return;

            byte[] arrByte = map.ObjectToByteArray();
            GameSave save = new GameSave()
            {
                Date = DateTime.Now,
                Scene = arrByte,
                Hash = hash
            };

            db.GameSaves.Add(save);
            db.SaveChanges();
        }

        public void DeleteGameSave(int id)
        {
            var result = db.GameSaves.Find(id);
            db.GameSaves.Remove(result);
            db.SaveChanges();
        }

        public Map LoadGameSave(int id)
        {
            GameSave save = db.GameSaves.Find(id);
            Map map = (Map)save.Scene.ByteArrayToObject();
            return map;
        }

        public Map LoadRandomGameSave()
        {
            List<int> ids = db.GameSaves.Select(x => x.ID).ToList();
            int count = ids.Count();
            Random rnd = new Random();
            int randomID = ids[rnd.Next(count)];
            GameSave save = db.GameSaves.Find(randomID);
            Map map = (Map)save.Scene.ByteArrayToObject();
            return map;
        }

    }
}
