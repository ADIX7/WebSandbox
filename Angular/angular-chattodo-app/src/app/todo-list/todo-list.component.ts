import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoItem } from '../models/todo-item';
import { TodoItemViewModel } from '../models/todo-item-vm';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  constructor(private http: HttpClient) { }

  public todos: TodoItemViewModel[] = [];
  private eventSource!: EventSource;

  @ViewChild('newTodo', { static: true }) newTodoElement!: ElementRef<HTMLInputElement>;

  ngOnInit(): void {
    this.updateTodos();

    this.eventSource = new EventSource('http://localhost:5001/server-events');
    this.eventSource.addEventListener('update-todos', (event: MessageEvent) => {
      this.updateTodos();
    })
  }

  private updateTodos(): void {
    this.http.get('http://localhost:5001/api/todo').subscribe(response => {
      this.todos = (response as TodoItem[]).map(item => {
        return {
          id: item.id,
          text: item.text
        } as TodoItemViewModel;
      });
    });
  }

  public addTodo(text: string): void {
    const newTodo = {
      text: text
    } as TodoItem;

    this.newTodoElement.nativeElement.value = '';
    this.newTodoElement.nativeElement.focus();
    this.http.put('http://localhost:5001/api/todo', newTodo).subscribe(_ => { });
  }

  public edit(id: number): void {
    this.todos.find(item => item.id === id)!.isEditVisible = true;
  }

  public saveEdited(id: number, newText: string): void {
    const todo = this.todos.find(item => item.id === id);

    if (!todo) return;

    const updatedTodo = {
      id: todo.id,
      text: newText
    }
    this.http.post(`http://localhost:5001/api/todo`, updatedTodo).subscribe(_ => { });
  }

}
