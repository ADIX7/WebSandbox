using HtmxCSRazor;
using HtmxCSRazor.Services;
using HtmxCSRazor.Todo;
using Lib.AspNetCore.ServerSentEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSentEvents();

builder.Services.AddSingleton<MessageService>();
builder.Services.AddSingleton<TodoService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapServerSentEvents("/messages-sse");

app.MapRazorPages();

app.Run();
