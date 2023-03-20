using MyTodo.Data.Models;

namespace MyTodoAPI.ViewModel
{
    public class TodoRequestModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
  
        public DateTime DueTo { get; set; }

        public DateTime createdAt { get; set;}

        public DateTime createdBy { get; set; }

        public EnumProgressState ProgressState { get; set; }
    }

}
