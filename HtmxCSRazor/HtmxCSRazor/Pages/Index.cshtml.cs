using Htmx;
using HtmxCSRazor.Services;
using HtmxCSRazor.Todo;
using HtmxCSRazor.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxCSRazor.Pages;

public class IndexModel(MessageService messageService, TodoService todoService) : PageModel
{
    public IActionResult OnGet()
    {
        if (Request.IsHtmx())
        {
            throw new NotImplementedException();
        }

        return Page();
    }

    public IActionResult OnGetMessages()
    {
        return GetMessagesView();
    }

    public async Task<IActionResult> OnGetAddMessageAsync([FromQuery] string chatText)
    {
        await messageService.AddMessage(chatText);
        return GetMessagesView();
    }

    public IActionResult GetMessagesView()
    {
        return ViewComponent(typeof(MessageListViewComponent));
    }

    public IActionResult OnGetTodos()
    {
        return GetTodosView();
    }

    public async Task<IActionResult> OnGetAddTodoAsync([FromQuery] string newTodo)
    {
        await todoService.AddTodo(new NewTodoItem(newTodo));
        return GetTodosView();
    }

    public async Task<IActionResult> OnGetUpdateTodoAsync(
        [FromQuery] string todoNewValue,
        [FromQuery] string id)
    {
        await todoService.UpdateTodo(int.Parse(id), new NewTodoItem(todoNewValue));
        return GetTodosView();
    }

    private IActionResult GetTodosView()
    {
        return ViewComponent(typeof(TodoListViewComponent));
    }
}