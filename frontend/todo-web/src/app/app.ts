import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgClass } from '@angular/common';
import { TodoItem } from './models/todo-item';
import { TodoService } from './services/todo.service';

@Component({
  selector: 'app-root',
  imports: [FormsModule, DatePipe, NgClass],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  tareas: TodoItem[] = [];
  textoBuscar = '';
  titulo = '';
  descripcion = '';
  cargando = false;
  mensaje = '';
  error = '';

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.consultarTodos();
  }

  consultarTodos(): void {
    this.cargando = true;
    this.error = '';

    this.todoService.consultarTodos(this.textoBuscar).subscribe({
      next: (data) => {
        this.tareas = data;
        this.cargando = false;
      },
      error: () => {
        this.error = 'No se pudieron cargar las tareas.';
        this.cargando = false;
      }
    });
  }

  guardarTodo(): void {
    const tituloLimpio = this.titulo.trim();

    if (!tituloLimpio) {
      this.error = 'El título es obligatorio.';
      return;
    }

    this.cargando = true;
    this.error = '';

    this.todoService.guardarTodo({
      titulo: tituloLimpio,
      descripcion: this.descripcion.trim() || null
    }).subscribe({
      next: () => {
        this.titulo = '';
        this.descripcion = '';
        this.mensaje = 'Tarea agregada correctamente.';
        this.consultarTodos();
      },
      error: () => {
        this.error = 'No se pudo guardar la tarea.';
        this.cargando = false;
      }
    });
  }

  cambiarEstado(tarea: TodoItem): void {
    this.todoService.cambiarEstado(tarea.id, !tarea.completada).subscribe({
      next: () => this.consultarTodos(),
      error: () => this.error = 'No se pudo cambiar el estado.'
    });
  }

  borrarTodo(tarea: TodoItem): void {
    const confirmar = confirm(`¿Desea eliminar la tarea "${tarea.titulo}"?`);

    if (!confirmar) {
      return;
    }

    this.todoService.borrarTodo(tarea.id).subscribe({
      next: () => {
        this.mensaje = 'Tarea eliminada correctamente.';
        this.consultarTodos();
      },
      error: () => this.error = 'No se pudo eliminar la tarea.'
    });
  }

  limpiarBusqueda(): void {
    this.textoBuscar = '';
    this.consultarTodos();
  }
}
