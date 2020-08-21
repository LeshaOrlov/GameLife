using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public class Log
    {
        [Key]
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public byte[] StartScene { get; set; }
        public byte[] FinishScene { get; set; }
    }
}
