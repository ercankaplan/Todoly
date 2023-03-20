using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTodoAPI.Dto;
using MyTodoAPI.Poviders;
using MyTodoAPI.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace MyTodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoItemProvider _todoProvider;

        private readonly ILogger<TodosController> _logger;

        public TodosController(ILogger<TodosController> logger, ITodoItemProvider todoProvider)
        {
            _logger = logger;
            _todoProvider = todoProvider;
        }

        [HttpGet("{id}")]
        public async Task<TodoItemDto> Get(Guid id)
        {
            return new TodoItemDto();
        }

        [HttpGet]
        public async Task<List<TodoItemDto>> List(Guid id)
        {
            return await _todoProvider.GetTodoItemsAsync();
        }

        [HttpPost]
        public async Task<TodoItemDto> Add([Required][FromBody] TodoRequestModel requestModel)
        { 
            return new TodoItemDto();
        }

        [HttpPut("{id}")]
        public async Task<TodoItemDto> Update([Required][FromBody] UpdateTodoRequestModel requestModel)
        {
            return  new TodoItemDto();
        }


        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return true;
        }

    }
}
