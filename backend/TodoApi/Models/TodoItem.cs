namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string? Descripcion { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
