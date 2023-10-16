using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PetaPoco;

namespace HtmxCSRazor.Todo;

// Uses PetaPoco and the official Microsoft.Data.SqlClient
public class MsSqlTodoRepository(IOptions<TodoConfiguration> configuration) : ITodoRepository
{
    public void AddTodo(NewTodoItem todo)
    {
        using var connection = new SqlConnection(configuration.Value.ConnectionStringMssql);
        connection.Open();

        using var database = new Database(connection);
        
        database.Execute("INSERT INTO Todos (Text) VALUES (@0)", todo.Text);
    }

    public IEnumerable<TodoItem> GetTodos()
    {
        using var connection = new SqlConnection(configuration.Value.ConnectionStringMssql);
        connection.Open();

        using var database = new Database(connection);
        var todos = database.Query<TodoItemEntity>("SELECT * FROM Todos")
            .Select(t => t.ToTodoItem())
            .ToList();

        return todos;
    }

    public void UpdateTodo(int id, NewTodoItem newTodoItem)
    {
        using var connection = new SqlConnection(configuration.Value.ConnectionStringMssql);
        connection.Open();

        using var database = new Database(connection);
        
        database.Execute("UPDATE Todos SET Text = @0 WHERE Id = @1", newTodoItem.Text, id);
    }
}