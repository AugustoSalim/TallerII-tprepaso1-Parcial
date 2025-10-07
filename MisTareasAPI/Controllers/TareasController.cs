using Microsoft.AspNetCore.Mvc;
using TareaApi;
using AccesoADatos;

namespace TareaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        // ============================================================================================
        // 🔹 INSTANCIA DE MANEJO DE TAREAS
        // ============================================================================================
        // Creamos un objeto de la clase ManejoTareas que se encargará de la lógica de las tareas
        private readonly ManejoTareas manejoTareas;

        public TareasController()
        {
            manejoTareas = new ManejoTareas();
        }

        // ============================================================================================
        // 🔹 ENDPOINT POST (Crear nueva tarea)
        // ============================================================================================
        // POST: api/tareas
        [HttpPost]
        public IActionResult CrearTarea([FromBody] Tarea tarea)
        {
            if (tarea == null)
                return BadRequest("La tarea no puede ser nula.");

            var nuevaTarea = manejoTareas.AgregarTarea(tarea.Titulo, tarea.Descripcion);
            return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = nuevaTarea.Id }, nuevaTarea);
        }

        // ============================================================================================
        // 🔹 ENDPOINT GET (Obtener tarea por ID)
        // ============================================================================================
        // GET: api/tareas/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerTareaPorId(int id)
        {
            var tarea = manejoTareas.ObtenerTareaPorId(id);
            if (tarea == null)
                return NotFound($"No se encontró la tarea con ID {id}.");

            return Ok(tarea);
        }

        // ============================================================================================
        // 🔹 ENDPOINT PUT (Actualizar tarea)
        // ============================================================================================
        // PUT: api/tareas/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarTarea(int id, [FromBody] Tarea tareaActualizada)
        {
            if (tareaActualizada == null)
                return BadRequest("Datos de la tarea inválidos.");

            // Llamamos al método que primero muestra todas las tareas y luego actualiza
            bool resultado = manejoTareas.ActualizarTarea(id, tareaActualizada.Titulo, tareaActualizada.Descripcion, tareaActualizada.Estado);

            if (!resultado)
                return NotFound($"No se encontró la tarea con ID {id}.");

            return Ok($"Tarea con ID {id} actualizada correctamente.");
        }

        // ============================================================================================
        // 🔹 ENDPOINT DELETE (Eliminar tarea)
        // ============================================================================================
        // DELETE: api/tareas/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarTarea(int id)
        {
            // Llamamos al método que muestra todas las tareas antes de eliminar
            bool resultado = manejoTareas.EliminarTarea(id);

            if (!resultado)
                return NotFound($"No se encontró la tarea con ID {id}.");

            return Ok($"Tarea con ID {id} eliminada correctamente.");
        }

        // ============================================================================================
        // 🔹 ENDPOINT GET (Listar todas las tareas)
        // ============================================================================================
        // GET: api/tareas
        [HttpGet]
        public IActionResult ListarTareas()
        {
            var tareas = manejoTareas.ListarTodasTareas();
            return Ok(tareas);
        }

        // ============================================================================================
        // 🔹 ENDPOINT GET (Listar todas las tareas completadas)
        // ============================================================================================
        // GET: api/tareas/completadas
        [HttpGet("completadas")]
        public IActionResult ListarTareasCompletadas()
        {
            var tareasCompletadas = manejoTareas.ListarTareasCompletadas();
            return Ok(tareasCompletadas);
        }
    }
}
