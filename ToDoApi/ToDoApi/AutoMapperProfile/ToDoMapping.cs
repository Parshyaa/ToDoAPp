using AutoMapper;
using ToDoApi.BusinessObjects.Interface;
using ToDoApi.Entity;
using ToDoApi.Entity.Interface;

namespace ToDoApi.AutoMapperProfile
{
    public class ToDoMapping : Profile
    {
        public ToDoMapping()
        {
            CreateMap<IToDo, IToDoBo>();

            CreateMap<ToDo, IToDoBo>();

            CreateMap<IToDoBo, IToDo>();

            CreateMap<IToDoBo, ToDo>();
        }

    }
}
