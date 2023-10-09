using HtmxCSRazor.Todo;
using Microsoft.AspNetCore.Mvc;

namespace HtmxCSRazor.ViewComponents;

[ViewComponent]
public class TodoListViewComponent(TodoService todoService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(todoService.GetTodos());
    }
}