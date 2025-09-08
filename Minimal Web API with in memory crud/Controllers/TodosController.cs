using Microsoft.AspNetCore.Mvc;
using TodoApiMinimal.Data;
using TodoApiMinimal.Models;

namespace TodoApiMinimal.Controllers;

[ApiController]
[Route("api/todos")] // Sets the base route for all actions in this controller
public class TodosController : ControllerBase
{
    // GET: api/todos
    [HttpGet]
    public ActionResult<List<Todo>> GetTodos()
    {
        return Ok(TodoDb.Todos);
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public ActionResult<Todo> GetTodoById(int id)
    {
        var todo = TodoDb.Todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    // POST: api/todos
    [HttpPost]
    public ActionResult<Todo> CreateTodo(Todo todo)
    {
        var newId = TodoDb.Todos.Any() ? TodoDb.Todos.Max(t => t.Id) + 1 : 1;
        todo.Id = newId;

        TodoDb.Todos.Add(todo);

        // Returns a 201 Created status with the new todo and a Location header pointing to the new resource.
        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, Todo inputTodo)
    {
        var todo = TodoDb.Todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        todo.Title = inputTodo.Title;
        todo.IsCompleted = inputTodo.IsCompleted;

        return NoContent();
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        var todo = TodoDb.Todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        TodoDb.Todos.Remove(todo);

        return NoContent();
    }
}
