namespace HtmxCSRazor.Todo;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todos = new();

    public void AddTodo(NewTodoItem todo)
    {
        var id = _todos.Count == 0 ? 1 : _todos.Select(t => t.Id).Max() + 1;
        _todos.Add(new TodoItem(todo.Text, id));
    }

    public IEnumerable<TodoItem> GetTodos() => _todos;

    public void UpdateTodo(int id, NewTodoItem newTodoItem)
    {
        _todos[_todos.FindIndex(t => t.Id == id)] = new TodoItem(newTodoItem.Text, id);
    }
}