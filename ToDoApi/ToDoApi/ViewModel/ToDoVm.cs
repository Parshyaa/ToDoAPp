using System;
using ToDoApi.BusinessObjects.Interface;

namespace ToDoApi.ViewModel
{
    public class ToDoVm : IToDoBo
    {
        public long ToDoId { get; set; }

        public string TaskName { get; set; }

        public bool IsComplete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
