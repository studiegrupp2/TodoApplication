
using Microsoft.EntityFrameworkCore;

namespace todoapp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();

            builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(
                "Host=localhost;Database=tododb;Username=postgres;Password=password"
            );
        });
        //övning 9
        //builder.Services.AddSingleton<CounterService>();

        // builder.Services.AddSingleton<WordService>();
        builder.Services.AddScoped<TodoService, TodoService>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

      
        app.MapControllers();
        app.Run();
    }
}