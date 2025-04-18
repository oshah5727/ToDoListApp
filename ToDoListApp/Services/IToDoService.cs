using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoItem>> GetTasksAsync();
        Task AddTaskAsync(ToDoItem task);
        Task UpdateTaskAsync(ToDoItem task);
        Task DeleteTaskAsync(string taskId);
    }
}
