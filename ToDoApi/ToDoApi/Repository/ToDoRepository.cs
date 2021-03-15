using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Context;
using ToDoApi.Entity;
using ToDoApi.Entity.Interface;
using ToDoApi.Repository.Interface;

namespace ToDoApi.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly IServiceScope _scope;
        private readonly ToDoDbContext _databaseContext;

        public ToDoRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
        }

        public IOrderedQueryable<IToDo> GetAll()
        {
            var result = _databaseContext.ToDoSet.OrderByDescending(x => x.ToDoId);

            return result;
        }

        public IToDo Get(long todoId)
        {
            var result = _databaseContext.ToDoSet.Where(x => x.ToDoId == todoId).FirstOrDefault();
            return result;
        }

        public async Task<bool> Create(ToDo toDo)
        {
            var success = false;

            _databaseContext.ToDoSet.Add(toDo);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;

            return success;

        }

        public async Task<bool> Delete(long todoId)
        {
            var success = false;

            var existingToDo = Get(todoId);

            if (existingToDo != null)
            {
                _databaseContext.ToDoSet.Remove((ToDo)existingToDo);

                var numberOfItemsDeleted = await _databaseContext.SaveChangesAsync();

                if (numberOfItemsDeleted == 1)
                    success = true;
            }

            return success;
        }

        public async Task<bool> Update(ToDo toDo)
        {
            var success = false;

            var existingToDo = Get(toDo.ToDoId);

            if (existingToDo != null)
            {
                existingToDo.IsComplete = toDo.IsComplete;
                existingToDo.TaskName = toDo.TaskName;
                existingToDo.UpdateDate = toDo.UpdateDate;

                _databaseContext.ToDoSet.Attach((ToDo)existingToDo);

                var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

                if (numberOfItemsUpdated == 1)
                    success = true;
            }

            return success;
        }
    }
}
