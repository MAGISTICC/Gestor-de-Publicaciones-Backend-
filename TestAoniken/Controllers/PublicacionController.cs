using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestAoniken.Models;

namespace TestAoniken.Controllers
{
    [Route("api/publicaciones")] // Define la ruta base para todas las acciones en este controlador
    [ApiController] // Indica que este controlador responde a solicitudes web API
    public class PublicacionController : ControllerBase
    {
        private readonly ApplicationDbContext _contexto; // Instancia del DbContext para interactuar con la base de datos

        public PublicacionController(ApplicationDbContext contexto)
        {
            _contexto = contexto; // Inicializa el contexto de la base de datos mediante inyección de dependencias
        }

    [HttpGet]
    [Route("listas")]
    public dynamic listarUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario
            {
                Id = 0,
                Nombre = "Marco",
                Rol = "Usuario"
            }
        };

        return usuarios;
    }
        


        // Método para obtener las publicaciones pendientes de aprobación
        [HttpGet("pendientes")]
        public IActionResult ObtenerPublicacionesPendientes()
        {
            var publicacionesPendientes = _contexto.Publicaciones
                .Where(p => p.PendienteAprobacion) // Filtra las publicaciones pendientes de aprobación
                .Select(p => new
                {
                    p.Id,
                    NombreAutor = p.Autor.Nombre,
                    FechaEnvio = p.FechaEnvio
                }) // Proyecta solo los campos necesarios para la respuesta
                .ToList(); // Convierte los resultados en una lista

            return Ok(publicacionesPendientes); // Devuelve una respuesta HTTP 200 OK con la lista de publicaciones pendientes en formato JSON
        }

        // Método para aprobar una publicación pendiente
        [HttpPost("aprobar")]
        public IActionResult AprobarPublicacion([FromBody] int idPublicacion)
        {
            var publicacion = _contexto.Publicaciones.Find(idPublicacion); // Busca la publicación por su ID
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicación no se encuentra
            }

            publicacion.PendienteAprobacion = false; // Cambia el estado de aprobación de la publicación
            _contexto.SaveChanges(); // Guarda los cambios en la base de datos

            return Ok(); // Devuelve una respuesta HTTP 200 OK
        }

        // Método para rechazar una publicación pendiente
        [HttpPost("rechazar")]
        public IActionResult RechazarPublicacion([FromBody] int idPublicacion)
        {
            var publicacion = _contexto.Publicaciones.Find(idPublicacion); // Busca la publicación por su ID
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicación no se encuentra
            }

            _contexto.Publicaciones.Remove(publicacion); // Elimina la publicación de la base de datos
            _contexto.SaveChanges(); // Guarda los cambios en la base de datos

            return Ok(); // Devuelve una respuesta HTTP 200 OK
        }

        // Método para actualizar una publicación existente
        [HttpPut("{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] Publicacion publicacionActualizada)
        {
            var publicacion = _contexto.Publicaciones.Find(id); // Busca la publicación por su ID
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicación no se encuentra
            }

            publicacion.Titulo = publicacionActualizada.Titulo; // Actualiza el título de la publicación
            publicacion.Contenido = publicacionActualizada.Contenido; // Actualiza el contenido de la publicación
            // Puedes actualizar otras propiedades según sea necesario

            _contexto.SaveChanges(); // Guarda los cambios en la base de datos

            return Ok(publicacion); // Devuelve una respuesta HTTP 200 OK con la publicación actualizada en formato JSON
        }

        // Método para eliminar una publicación existente
        [HttpDelete("{id}")]
        public IActionResult EliminarPublicacion(int id)
        {
            var publicacion = _contexto.Publicaciones.Find(id); // Busca la publicación por su ID
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicación no se encuentra
            }

            _contexto.Publicaciones.Remove(publicacion); // Elimina la publicación de la base de datos
            _contexto.SaveChanges(); // Guarda los cambios en la base de datos

            return Ok(); // Devuelve una respuesta OK
        }
    }
}
