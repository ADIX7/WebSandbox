using Lib.AspNetCore.ServerSentEvents;

namespace CommonWebApi.Todo;

public class TodoService(IServerSentEventsService serverSentEventsService)
{
    private readonly Dictionary<int, TodoItem> _todos = new();

    public async Task AddTodo(NewTodoItem todo)
    {
        var id = _todos.Count == 0 ? 1 : _todos.Keys.Max() + 1;
        _todos.Add(id, new TodoItem(todo.Text, id));

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }

    public IEnumerable<TodoItem> GetTodos() => _todos.Values;

    public async Task<bool> UpdateTodo(TodoItem newTodoItem)
    {
        if (!_todos.ContainsKey(newTodoItem.Id)) return false;
        _todos[newTodoItem.Id] = newTodoItem;

        //Note: the sender can be excluded. In that case the client side should handle updating the value with the new one.
        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });

        return true;
    }
}