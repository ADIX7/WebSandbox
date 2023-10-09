using Htmx;
using HtmxCSRazor.Services;
using HtmxCSRazor.Todo;
using HtmxCSRazor.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxCSRazor.Pages;

public class IndexModel(MessageService messageService, TodoService todoService) : PageModel
{
    public IEnumerable<string> Messages => messageService.GetMessages();

    public async Task<IActionResult> OnGetAsync(
        [FromQuery] string? chatText,
        [FromQuery] string? newTodo,
        [FromQuery] string? todoNewValue,
        [FromQuery] string? id,
        [FromHeader(Name = "HX-Target")] string? hxTarget
    )
    {
        if (Request.IsHtmx())
        {
            if (hxTarget == "messages")
            {
                if (chatText is not null)
                {
                    await messageService.AddMessage(chatText);
                }

                return ViewComponent(GetViewComponentName<MessageListViewComponent>());
            }

            if (hxTarget == "todos")
            {
                if (newTodo is not null)
                {
                    await todoService.AddTodo(new NewTodoItem(newTodo));
                }
                else if(todoNewValue is not null && id is not null)
                {
                    await todoService.UpdateTodo(int.Parse(id), new NewTodoItem(todoNewValue));
                }

                return ViewComponent(GetViewComponentName<TodoListViewComponent>());
            }

            throw new NotImplementedException();
        }

        return Page();
    }

    private string GetViewComponentName<T>() where T : ViewComponent => typeof(T).Name.Replace("ViewComponent", "");
}