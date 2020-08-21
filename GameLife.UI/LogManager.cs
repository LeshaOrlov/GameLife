using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    class LogManager: ILogManager
    {
        private EntityContext db = new EntityContext();
        Log log;
        public List<LogDTO> GetLogs()
        {
            var result = db.Logs.Select(x => new LogDTO
            {
                ID = x.ID,
                StartDate = x.StartDate,
                FinishDate = x.FinishDate
            }).ToList();
            return result;
        }

        public void StartLog(Map map)
        {
            byte[] arrByte = map.ObjectToByteArray();
            log = new Log()
            {
                StartDate = DateTime.Now,
                StartScene = arrByte
            };
        }

        public void SaveLog(Map map)
        {
            byte[] arrByte = map.ObjectToByteArray();
            log.FinishDate = DateTime.Now;
            log.FinishScene = arrByte;
            db.Logs.Add(log);
            db.SaveChanges();
        }
    }
}
