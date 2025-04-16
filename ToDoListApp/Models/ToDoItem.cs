using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    class ToDoItem
    {
        string Name { get; set; }
        string Description { get; set; }
        string DueBy { get; set; }
        Boolean IsComplete { get; set; }

    }
}
