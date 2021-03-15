using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApi.Bll.Interface;
using ToDoApi.ViewModel;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly IToDoBll _toDoBll;

        public ToDoController(IToDoBll toDoBll)
        {
            this._toDoBll = toDoBll;
        }

        [HttpGet("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _toDoBll.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(long id)
        {
            if (id != 0)
            {
                var result = _toDoBll.Get(id);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ToDoVm todo)
        {
            if (todo != null)
            {
                var result = await _toDoBll.Create(todo);

                return Ok(result);
            }
            else
            {
                return BadRequest("Model Invalid!");
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] ToDoVm todo)
        {
            if (todo != null)
            {
                var result = await _toDoBll.Update(todo);

                return Ok(result);
            }
            else
            {
                return BadRequest("Model Invalid!");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {
            if (id != 0)
            {
                var result = await _toDoBll.Delete(id);

                return Ok(result);
            }
            else
            {
                return BadRequest("Please Specify the Id to delete");
            }
        }

        [HttpPost("complete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkComplete([FromBody] ToDoVm todo)
        {
            if (todo != null)
            {
                var result = await _toDoBll.CompleteToDo(todo);

                return Ok(result);
            }
            else
            {
                return BadRequest("Model Invalid!");
            }
        }
    }
}
