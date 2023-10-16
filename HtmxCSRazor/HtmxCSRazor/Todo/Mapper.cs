using Riok.Mapperly.Abstractions;

namespace HtmxCSRazor.Todo;

[Mapper]
public static partial class Mapper
{
    public static partial TodoItem ToTodoItem(this TodoItemEntity entity);

}