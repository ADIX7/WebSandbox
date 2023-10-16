using HtmxCSRazor.Services;
using HtmxCSRazor.Todo;
using Lib.AspNetCore.ServerSentEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSentEvents();

builder.Services.AddSingleton<MessageService>();
builder.Services.AddSingleton<TodoService>();
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
// Preconfigured Docker Compose and appsettings.Development.json is available for these repositories
// Sql repositories use PetaPoco, a micro-ORM for mapping SQL queries to objects, but beside that, they use plain SQL
//builder.Services.AddSingleton<ITodoRepository, PostgresTodoRepository>();
//builder.Services.AddSingleton<ITodoRepository, MsSqlTodoRepository>();

builder.Services.AddOptions<TodoConfiguration>()
    .Bind(builder.Configuration.GetSection("Todo"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapServerSentEvents("/messages-sse");

app.MapRazorPages();

app.Run();
