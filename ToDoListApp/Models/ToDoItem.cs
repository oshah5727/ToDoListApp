using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DueBy { get; set; }
        public bool IsComplete { get; set; }

    }
}
