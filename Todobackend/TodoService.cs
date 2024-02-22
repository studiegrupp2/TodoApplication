namespace todoapp;

using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

public interface ITodoService
{
    public void AddTodo(string title);
    public List<Todo> GetTodo();
    public void UpdateTodo(int id, bool completed);

    public void DeleteTodo(int id);
}
public class Todo
{
    private static int ID_COUNTER = 0;
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
    public Todo(string title)
    {
        this.Title = title;
        this.Id = ID_COUNTER++;
        this.Completed = false;
    }    
}
public class TodoService : ITodoService
{
    DatabaseContext context;

    public TodoService(DatabaseContext context)
    {
        this.context = context;
    }
    
    //List<Todo> todos = new List<Todo>();
    public void AddTodo(string title)
    {
        Todo todo = new Todo(title);
        context.Add(todo);
        context.SaveChanges();
    }

    public void DeleteTodo(int id)
    {
       Todo? todo = context.Todos.Find(id);
       if (todo == null)
       {
        return;
       }
       context.Todos.Remove(todo);
       context.SaveChanges();
    }

    public List<Todo> GetTodo()
    {
        return context.Todos.ToList();   
    }

    public void UpdateTodo(int id, bool completed)
    {
        Todo? todo = context.Todos.Find(id);
        if(todo == null){
            return;
        }
        
        todo.Completed = completed;
        context.SaveChanges();
    }
}

public class DatabaseContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }
}

[ApiController]
[Route("todos")]
public class TodoController : ControllerBase
{
    private TodoService service;
    public TodoController(TodoService todoService)
    {
        this.service = todoService;
    }

    [HttpPost]
    public IActionResult AddTodo([FromQuery] string title)
    {
        service.AddTodo(title);
        return Ok("lyckades");
    }

    [HttpGet]
    public List<Todo> GetAllTodos()
    {
        return service.GetTodo();
    }

    [HttpPut("todo/{id}")]
    public IActionResult UpdateTodo (int id, [FromQuery] bool completed)
    {
        service.UpdateTodo(id, completed);
        return Ok();
    }

    [HttpDelete("todo/{id}")]
    public IActionResult DeleteTodo(int id)
    {
        service.DeleteTodo(id);
        return Ok();
    }
}