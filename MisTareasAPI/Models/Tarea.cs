namespace TareaApi
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public EstadoTarea Estado { get; set; }

        public Tarea()
        {
            Titulo = "";
            Descripcion = "";
            Estado = EstadoTarea.Pendiente;
        }

        public Tarea(int id, string titulo, string descripcion)
        {
            Id = id;
            Titulo = titulo;
            Descripcion = descripcion;
            Estado = EstadoTarea.Pendiente;
        }

        
    }
}