using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entity;
using ToDoApi.Entity.Interface;

namespace ToDoApi.Repository.Interface
{
    public interface IToDoRepository
    {
        Task<bool> Create(ToDo toDo);

        Task<bool> Update(ToDo toDo);

        IToDo Get(long todoId);

        IOrderedQueryable<IToDo> GetAll();

        Task<bool> Delete(long todoId);
    }
}
