namespace HtmxCSRazor.Todo;

public interface ITodoRepository
{
    void AddTodo(NewTodoItem todo);
    IEnumerable<TodoItem> GetTodos();
    void UpdateTodo(int id, NewTodoItem newTodoItem);
}