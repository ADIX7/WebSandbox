using HtmxCSRazor.Services;
using Microsoft.AspNetCore.Mvc;

namespace HtmxCSRazor.ViewComponents;

[ViewComponent]
public class MessageListViewComponent(MessageService messageService) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(messageService.GetMessages());
    }
}