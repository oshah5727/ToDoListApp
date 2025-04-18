using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class ToDoService : IToDoService
    {
        private static List<ToDoItem> _tasks = new List<ToDoItem>();

        public Task<IEnumerable<ToDoItem>> GetTasksAsync()
        {
            return Task.FromResult<IEnumerable<ToDoItem>>(_tasks.ToList());
        }

        public Task AddTaskAsync(ToDoItem task)
        {
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task UpdateTaskAsync(ToDoItem task)
        {
            var index = _tasks.FindIndex(t => t.Name == task.Name);
            if (index != -1)
            {
                _tasks[index] = task;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTaskAsync(string name)
        {
            var task = _tasks.FirstOrDefault(t => t.Name == name);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            return Task.CompletedTask;
        }
    }

}
