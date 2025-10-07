using System.Text.Json;
using TareaApi;

namespace AccesoADatos
{
    public class AccesoADatos
    {
        private readonly string rutaArchivo = "Data/tareas.json";


        public List<Tarea> ObtenerTareas()
        {
            if (!File.Exists(rutaArchivo))
            {
                return new List<Tarea>();
            }

            string json = File.ReadAllText(rutaArchivo);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Tarea>();
            }
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
                
            return JsonSerializer.Deserialize<List<Tarea>>(json, opciones) ?? new List<Tarea>();
        }

        public void GuardarTareas(List<Tarea> tareas)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tareas, opciones);
            Directory.CreateDirectory("Data");
            File.WriteAllText(rutaArchivo, json);
        }
    }   
}