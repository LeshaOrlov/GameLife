using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public class LogDTO
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

    }
}
