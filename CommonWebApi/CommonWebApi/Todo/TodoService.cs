using Lib.AspNetCore.ServerSentEvents;

namespace CommonWebApi.Todo;

public class TodoService(IServerSentEventsService serverSentEventsService)
{
    private readonly List<TodoItem> _todos = new()
    {
        new TodoItem("Learn HTMX", 1),
        new TodoItem("Learn Tailwind", 2),
        new TodoItem("Learn Hotwire", 3),
    };

    public async Task AddTodo(NewTodoItem todo)
    {
        var id = _todos.Count == 0 ? 1 : _todos.Select(t => t.Id).Max() + 1;
        _todos.Add(new TodoItem(todo.Text, id));

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }

    public IEnumerable<TodoItem> GetTodos() => _todos;

    public async Task UpdateTodo(TodoItem newTodoItem)
    {
        _todos[_todos.FindIndex(t => t.Id == newTodoItem.Id)] = newTodoItem;

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }
}