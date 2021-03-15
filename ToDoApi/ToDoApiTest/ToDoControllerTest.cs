using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApi.Bll.Interface;
using ToDoApi.BusinessObjects;
using ToDoApi.Controllers;
using ToDoApi.ViewModel;
using Xunit;

namespace ToDoApiTest
{
    public class ToDoControllerTest
    {
        private Mock<IToDoBll> _toDoBll;
        private readonly IList<ToDoBo> _toDoList;
        private ToDoController _toDoController;

        public ToDoControllerTest()
        {
            _toDoList = new List<ToDoBo>()
            {
                new ToDoBo() { ToDoId = 111, TaskName = "Create a project for Todo", IsComplete=true, CreateDate=DateTime.UtcNow, UpdateDate=DateTime.UtcNow },
                new ToDoBo() { ToDoId = 112, TaskName = "Create a Test Project FOr TODO API", IsComplete=true, CreateDate=DateTime.UtcNow, UpdateDate=DateTime.UtcNow  },
                new ToDoBo() { ToDoId = 114, TaskName = "Create a Web Project", IsComplete=false, CreateDate=DateTime.UtcNow.AddDays(2), UpdateDate=DateTime.UtcNow.AddDays(2)  },
                new ToDoBo() { ToDoId = 113, TaskName = "Upload the project", IsComplete=false, CreateDate=DateTime.UtcNow.AddDays(4), UpdateDate=DateTime.UtcNow.AddDays(3)  },
            };
            Setup();
        }

        private void Setup()
        {
            _toDoBll = new Mock<IToDoBll>();

            _toDoBll.Setup(o => o.GetAll()).Returns(_toDoList);
            _toDoBll.Setup(t => t.Get(112)).Returns(_toDoList.FirstOrDefault(t => t.ToDoId == 112));

            _toDoController = new ToDoController(_toDoBll.Object);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var okResult = _toDoController.GetAll();

            _toDoBll.Verify(s => s.GetAll(), Times.Once());
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsAllItems()
        {
            var okResult = _toDoController.GetAll() as OkObjectResult;

            var items = Assert.IsType<List<ToDoBo>>(okResult.Value);
            _toDoBll.Verify(s => s.GetAll(), Times.Once());

            Assert.Equal(4, items.Count);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsToDoObjectFromTheList()
        {
            var okResult = _toDoController.GetById(112) as OkObjectResult;

            var items = Assert.IsType<ToDoBo>(okResult.Value);
            _toDoBll.Verify(s => s.Get(112), Times.Once());

            Assert.Equal(112, items.ToDoId);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsOneToDoObjectFromtheList()
        {
            var toDoItem = new ToDoVm {  TaskName = "Mail the Project", IsComplete = false, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };
            var toDoBo = new ToDoBo {ToDoId= 119, TaskName = "Mail the Project", IsComplete = false, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };

            _toDoBll.Setup(t => t.Create(toDoItem)).ReturnsAsync(toDoBo);

            var okObject = _toDoController.Create(toDoItem);
            var okResult = okObject.Result as OkObjectResult;
                
            var items = Assert.IsType<ToDoBo>(okResult.Value);
            _toDoBll.Verify(s => s.Create(toDoItem), Times.Once());

            Assert.Equal(toDoBo.ToDoId, items.ToDoId);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsBadRequest()
        {
            var toDoBo = new ToDoBo { ToDoId = 119, TaskName = "Mail the Project", IsComplete = false, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };

            _toDoBll.Setup(t => t.Create(null)).ReturnsAsync(toDoBo);

            var badObject = _toDoController.Create(null);
            var badResult = badObject.Result as BadRequestObjectResult;

            var items = Assert.IsType<string>(badResult.Value);
            _toDoBll.Verify(s => s.Create(null), Times.Never());

            Assert.Equal("Model Invalid!", items); 
          Assert.Equal(400, badResult.StatusCode);

        }

        [Fact]
        public void Delete_WhenCalled_ReturnsOkResultWithTrue()
        {
            _toDoBll.Setup(t => t.Delete(119)).ReturnsAsync(true);

            var okObject = _toDoController.Delete(119);
            var okResult = okObject.Result as OkObjectResult;

            var items = Assert.IsType<bool>(okResult.Value);
            _toDoBll.Verify(s => s.Delete(119), Times.Once());

            Assert.True((bool)items);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsBadRequest()
        {

            var badObject = _toDoController.Delete(0);
            var badResult = badObject.Result as BadRequestObjectResult;

            var items = Assert.IsType<string>(badResult.Value);
            _toDoBll.Verify(s => s.Delete(0), Times.Never());

            Assert.Equal("Please Specify the Id to delete", items);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        public void Update_WhenCalled_ReturnsOneTODoObjectFromtheListWhichWasUpdated()
        {
            var toDoItem = new ToDoVm { ToDoId = 119, TaskName = "Mail the Project asap", IsComplete = false, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };
            var toDoBo = new ToDoBo { ToDoId = 119, TaskName = "Mail the Project asap", IsComplete = false, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };

            _toDoBll.Setup(t => t.Update(toDoItem)).ReturnsAsync(toDoBo);

            var okObject = _toDoController.Update(toDoItem);
            var okResult = okObject.Result as OkObjectResult;

            var items = Assert.IsType<ToDoBo>(okResult.Value);
            _toDoBll.Verify(s => s.Update(toDoItem), Times.Once());

            Assert.Equal(toDoBo.ToDoId, items.ToDoId);
            Assert.Equal(toDoBo.TaskName, items.TaskName);
        }

        [Fact]
        public void MarkComplete_WhenCalled_ReturnsOneTODoObjectFromtheListWhichWasMarkedAsCompleted()
        {
            var toDoItem = new ToDoVm { ToDoId = 119, TaskName = "Mail the Project asap", IsComplete = true, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };
            var toDoBo = new ToDoBo { ToDoId = 119, TaskName = "Mail the Project asap", IsComplete = true, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };

            _toDoBll.Setup(t => t.CompleteToDo(toDoItem)).ReturnsAsync(toDoBo);

            var okObject = _toDoController.MarkComplete(toDoItem);
            var okResult = okObject.Result as OkObjectResult;

            var items = Assert.IsType<ToDoBo>(okResult.Value);
            _toDoBll.Verify(s => s.CompleteToDo(toDoItem), Times.Once());

            Assert.Equal(toDoBo.ToDoId, items.ToDoId);
            Assert.Equal(toDoBo.TaskName, items.TaskName);
            Assert.Equal(toDoBo.IsComplete, items.IsComplete);

        }
    }
}
