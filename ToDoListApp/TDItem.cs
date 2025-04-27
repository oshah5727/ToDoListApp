using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ToDoListApp
{
    public class TDItem
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }
       
        [Column("description")]
        public string Description { get; set; }

        [Column("date")]
        public string Date { get; set; }
    }
}
