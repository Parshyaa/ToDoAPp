using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.BusinessObjects;
using ToDoApi.BusinessObjects.Interface;

namespace ToDoApi.Bll.Interface
{
    public interface IToDoBll
    {
        Task<IToDoBo> Create(IToDoBo todo);

        Task<IToDoBo> Update(IToDoBo todo);

        IToDoBo Get(long toDoId);

        IEnumerable<IToDoBo> GetAll();

        Task<bool> Delete(long toDoId);

        Task<IToDoBo> CompleteToDo(IToDoBo todo);
    }
}
