using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ToDoListApp.Models
{
    [Table("TDItem")]
    public class ToDoItem
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id") ]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("due_by")]
        public string DueBy { get; set; }
        [Column("is_complete")]
        public bool IsComplete { get; set; }

    }
}
