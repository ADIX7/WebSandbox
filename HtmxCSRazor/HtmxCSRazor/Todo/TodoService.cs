using Lib.AspNetCore.ServerSentEvents;

namespace HtmxCSRazor.Todo;

public class TodoService(IServerSentEventsService serverSentEventsService)
{
    private readonly List<TodoItem> _todos = new();

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

    public async Task UpdateTodo(int id, NewTodoItem newTodoItem)
    {
        _todos[_todos.FindIndex(t => t.Id == id)] = new TodoItem(newTodoItem.Text, id);

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }
}