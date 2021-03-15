using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Bll.Interface;
using ToDoApi.BusinessObjects;
using ToDoApi.BusinessObjects.Interface;
using ToDoApi.Entity;
using ToDoApi.Entity.Interface;
using ToDoApi.Repository.Interface;

namespace ToDoApi.Bll
{
    public class ToDoBll : IToDoBll
    {
        private readonly IMapper _mapper;
        private readonly IToDoRepository _repository;

        public ToDoBll(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IToDoBo Get(long toDoId)
        {
            var result = _repository.Get(toDoId);

            return _mapper.Map<IToDoBo>(result);
        }

        public IEnumerable<IToDoBo> GetAll()
        {
            var result = _repository.GetAll();

            return _mapper.Map<IEnumerable<IToDoBo>>(result);
        }

        public async Task<IToDoBo> CompleteToDo(IToDoBo todo)
        {
            var todoDe = _mapper.Map<ToDo>(todo);
            todoDe.UpdateDate = DateTime.UtcNow;

            var success = await _repository.Update(todoDe);

            if (success)
            {
                return todo;
            }
            else
            {
                return null;
            }
        }

        public async Task<IToDoBo> Create(IToDoBo todo)
        {
            var todoDe = _mapper.Map<ToDo>(todo);

            todoDe.CreateDate = DateTime.UtcNow;
            todoDe.UpdateDate = DateTime.UtcNow;

            var success = await _repository.Create(todoDe);

            if (success)
            {
                return todo;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Delete(long toDoId)
        {
            var success = await _repository.Delete(toDoId);

            return success;
        }


        public async Task<IToDoBo> Update(IToDoBo todo)
        {
            var todoDe = _mapper.Map<ToDo>(todo);
            todoDe.UpdateDate = DateTime.UtcNow;

            var success = await _repository.Update(todoDe);

            if (success)
            {
                return todo;
            }
            else
            {
                return null;
            }
        }
    }
}

