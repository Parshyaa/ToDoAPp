using System;

namespace ToDoApi.BusinessObjects.Interface
{
    public interface IToDoBo
    {
        long ToDoId { get; set; }

        string TaskName { get; set; }

        bool IsComplete { get; set; }

        DateTime CreateDate { get; set; }

        DateTime UpdateDate { get; set; }
    }
}
