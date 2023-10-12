using CommonWebApi.Todo;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServerSentEvents();

builder.Services.AddCors();

builder.Services.AddSingleton<TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapServerSentEvents("/server-events");

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/api/todo", ([FromServices] TodoService todoService) => todoService.GetTodos());
app.MapPut("/api/todo",
    ([FromServices] TodoService todoService, [FromBody] NewTodoItem newTodoItem)
        => todoService.AddTodo(newTodoItem)
);
app.MapPost("/api/todo",
    [SwaggerResponse(200)]
    [SwaggerResponse(404)]
    async ([FromServices] TodoService todoService, [FromBody] TodoItem updatedTodo)
        =>
    {
        if (!await todoService.UpdateTodo(updatedTodo)) return Results.NotFound();
        return Results.Ok();
    });

app.Run();