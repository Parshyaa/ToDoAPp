using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApi.Entity.Interface
{
    public interface IToDo
    {
        long ToDoId { get; set; }

        string TaskName { get; set; }

        bool IsComplete { get; set; }

        DateTime CreateDate { get; set; }
        
        DateTime UpdateDate { get; set; }

    }
}
