using Microsoft.Extensions.Options;
using PetaPoco;

namespace HtmxCSRazor.Todo;

// Uses PetaPoco and Npgsql
public class PostgresTodoRepository(IOptions<TodoConfiguration> configuration) : ITodoRepository
{
    public void AddTodo(NewTodoItem todo)
    {
        using var database = new Database(configuration.Value.ConnectionStringPostgres, "Npgsql");
        
        database.Execute("INSERT INTO Todos (Text) VALUES (@0)", todo.Text);
    }

    public IEnumerable<TodoItem> GetTodos()
    {
        using var database = new Database(configuration.Value.ConnectionStringPostgres, "Npgsql");
        var todos = database.Query<TodoItemEntity>("SELECT * FROM Todos")
            .Select(t => t.ToTodoItem())
            .ToList();

        return todos;
    }

    public void UpdateTodo(int id, NewTodoItem newTodoItem)
    {
        using var database = new Database(configuration.Value.ConnectionStringPostgres, "Npgsql");
        
        database.Execute("UPDATE Todos SET Text = @0 WHERE Id = @1", newTodoItem.Text, id);
    }
}