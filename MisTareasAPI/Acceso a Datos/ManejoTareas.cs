using TareaApi;

namespace AccesoADatos
{
    public class ManejoTareas
    {
        private readonly AccesoADatos accesoDatos;
        private static int proximoId = 1; // ID autoincremental

        public ManejoTareas()
        {
            accesoDatos = new AccesoADatos();

            // Inicializar proximoId según las tareas que ya existan en el JSON
            var tareasExistentes = accesoDatos.ObtenerTareas();
            if (tareasExistentes.Count > 0)
            {
                proximoId = tareasExistentes.Max(t => t.Id) + 1;
            }
        }

        // Crear nueva tarea
        public Tarea AgregarTarea(string titulo, string descripcion)
        {
            var nuevaTarea = new Tarea(proximoId++, titulo, descripcion);
            var tareas = accesoDatos.ObtenerTareas();
            tareas.Add(nuevaTarea);
            accesoDatos.GuardarTareas(tareas);
            return nuevaTarea;
        }

        // Obtener tarea por ID
        public Tarea? ObtenerTareaPorId(int id)
        {
            var tareas = accesoDatos.ObtenerTareas();
            return tareas.FirstOrDefault(t => t.Id == id);
        }

        // Actualizar tarea por ID
        public bool ActualizarTarea(int id, string titulo, string descripcion, EstadoTarea estado)
        {
            var tareas = accesoDatos.ObtenerTareas();

            // Mostrar todas las tareas antes de actualizar
            Console.WriteLine("Tareas actuales:");
            foreach (var t in tareas)
            {
                Console.WriteLine($"ID: {t.Id}, Titulo: {t.Titulo}, Descripción: {t.Descripcion}, Estado: {t.Estado}");
            }

            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"No se encontró la tarea con ID {id}.");
                return false;
            }

            tarea.Titulo = titulo;
            tarea.Descripcion = descripcion;
            tarea.Estado = estado;

            accesoDatos.GuardarTareas(tareas);
            return true;
        }

        // Eliminar tarea por ID
        public bool EliminarTarea(int id)
        {
            var tareas = accesoDatos.ObtenerTareas();

            // Mostrar todas las tareas antes de eliminar
            Console.WriteLine("Tareas actuales:");
            foreach (var t in tareas)
            {
                Console.WriteLine($"ID: {t.Id}, Titulo: {t.Titulo}, Descripción: {t.Descripcion}, Estado: {t.Estado}");
            }

            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"No se encontró la tarea con ID {id}.");
                return false;
            }

            tareas.Remove(tarea);
            accesoDatos.GuardarTareas(tareas);
            return true;
        }


        // Listar todas las tareas
        public List<Tarea> ListarTodasTareas()
        {
            return accesoDatos.ObtenerTareas();
        }
        public bool CambiarEstadoTarea(int id, EstadoTarea nuevoEstado)
        {
            // Traemos la lista completa de tareas
            var tareas = accesoDatos.ObtenerTareas();

            // Buscamos la tarea dentro de la lista
            var tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
                return false;

            // Solo cambiamos el estado
            tarea.Estado = nuevoEstado;

            // Guardamos los cambios en el JSON
            accesoDatos.GuardarTareas(tareas); // ✅ Lista completa, no tarea individual

            return true;
        }

        // Listar solo tareas completadas
        public List<Tarea> ListarTareasCompletadas()
        {
            var tareas = accesoDatos.ObtenerTareas();
            return tareas.Where(t => t.Estado == EstadoTarea.Completada).ToList();
        }
    }
}
