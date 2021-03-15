using System;
using ToDoApi.Entity.Interface;

namespace ToDoApi.Entity
{
    public class ToDo : IToDo
    {
        public long ToDoId { get; set; }

        public string TaskName { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
