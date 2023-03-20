using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTodo.Data.Models.Ef
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public DateTime? UpdateTime { get; set; }
    }
}
