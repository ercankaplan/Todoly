using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTodo.Data.Models.Ef
{
    public class TodoItem:BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueTo { get; set; }

        public EnumProgressState ProgressState { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
