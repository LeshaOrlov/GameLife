using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public interface ILogManager
    {
        List<LogDTO> GetLogs();
        void StartLog(Map map);
        void SaveLog(Map map);

    }
}
