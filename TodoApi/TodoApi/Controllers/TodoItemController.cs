using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly TodoService _todoService;

    /// <summary>
    /// Instantiate the class.
    /// </summary>
    /// <param name="todoService"></param>
    public TodoItemController(TodoService todoService) => _todoService = todoService;
    
    [HttpGet]
    public async Task<List<TodoItem>> Get() => await _todoService.GetAsync();
    
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TodoItem>> Get(string id)
    {
        var book = await _todoService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoItem todoItem)
    {
        await _todoService.CreateAsync(todoItem);

        return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TodoItem todoItem)
    {
        var todo = await _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        todoItem.Id = todo.Id;

        await _todoService.UpdateAsync(id, todoItem);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        await _todoService.RemoveAsync(id);

        return NoContent();
    }
}
