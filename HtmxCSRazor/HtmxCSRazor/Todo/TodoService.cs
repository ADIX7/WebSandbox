using Lib.AspNetCore.ServerSentEvents;

namespace HtmxCSRazor.Todo;

public class TodoService(IServerSentEventsService serverSentEventsService, ITodoRepository todoRepository)
{

    public async Task AddTodo(NewTodoItem todo)
    {
        todoRepository.AddTodo(todo);

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }

    public IEnumerable<TodoItem> GetTodos() => todoRepository.GetTodos();

    public async Task UpdateTodo(int id, NewTodoItem newTodoItem)
    {
        todoRepository.UpdateTodo(id, newTodoItem);

        await serverSentEventsService.SendEventAsync(new ServerSentEvent
        {
            Id = "update-todos",
            Type = "update-todos",
            Data = new List<string> {""}
        });
    }
}