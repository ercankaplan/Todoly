using MyTodo.Data.Models.Ef;
using MyTodoAPI.Dto;

namespace MyTodoAPI.Poviders
{
    public interface ITodoItemProvider
    {
        Task<TodoItem> AddTodoItemAsync(TodoItemDto model);
        Task<TodoItem> UpdateTodoItemAsync(TodoItemDto model);
        Task<TodoItem> GetTodoItemAsync(Guid id);
        Task<List<TodoItemDto>> GetTodoItemsAsync();
        Task<bool> DeleteTodoItemAsync(Guid id);
       
  
    }
}
