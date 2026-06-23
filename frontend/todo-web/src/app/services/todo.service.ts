import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoItem } from '../models/todo-item';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private readonly apiUrl = 'https://localhost:7177/api/todos';

  constructor(private http: HttpClient) {}

  consultarTodos(textoBuscar: string = ''): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(`${this.apiUrl}?textoBuscar=${textoBuscar}`);
  }

  guardarTodo(todo: { titulo: string; descripcion?: string | null }): Observable<TodoItem> {
    return this.http.post<TodoItem>(this.apiUrl, todo);
  }

  actualizarTodo(id: number, todo: { titulo: string; descripcion?: string | null }): Observable<TodoItem> {
    return this.http.put<TodoItem>(`${this.apiUrl}/${id}`, todo);
  }

  cambiarEstado(id: number, completada: boolean): Observable<TodoItem> {
    return this.http.patch<TodoItem>(`${this.apiUrl}/${id}/estado`, { completada });
  }

  borrarTodo(id: number): Observable<{ mensaje: string }> {
    return this.http.delete<{ mensaje: string }>(`${this.apiUrl}/${id}`);
  }
}
