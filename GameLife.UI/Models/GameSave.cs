using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public class GameSave
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public byte[] Scene { get; set; }
        public string Hash { get; set; }
    }
}
