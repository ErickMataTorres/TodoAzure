export interface TodoItem {
  id: number;
  titulo: string;
  descripcion?: string | null;
  completada: boolean;
  fechaCreacion: string;
  fechaActualizacion?: string | null;
}
