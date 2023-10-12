import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TodoItem } from '../models/todo-item';
import { BehaviorSubject } from 'rxjs';
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

  ngOnInit(): void {
    this.http.get('http://localhost:5001/api/todos').subscribe(response => {
      console.log(response);
      this.todos = (response as TodoItem[]).map(item => {
        return {
          id: item.id,
          text: item.text
        } as TodoItemViewModel;
      });
    });

    this.eventSource = new EventSource('http://localhost:5001/server-events');
    this.eventSource.addEventListener('message', (event: MessageEvent) => {
      console.log("SSE", event);
    })
  }

  public onEditClick(id: number): void {
    this.todos.find(item => item.id === id)!.isEditVisible = true;
  }

  public onSaveClick(id: number): void {
    console.log('save clicked');
    const todo = this.todos.find(item => item.id === id);
    console.log(todo);

    if (!todo) return;
    console.log("sending");

    const updatedTodo = {
      id: todo.id,
      text: todo.text
    }
    this.http.post('http://localhost:5001/api/todo', updatedTodo).subscribe(_ => {});
  }

}
