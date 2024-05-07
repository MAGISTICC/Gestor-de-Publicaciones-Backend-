using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestAoniken.Models;

namespace TestAoniken.Controllers
{
    [Route("api/publicaciones")] // Define la ruta base para todas las acciones del controlador
    [ApiController] // Indica que el controlador responde a solicitudes HTTP API
    public class PublicacionController : ControllerBase
    {
        private readonly ApplicationDbContext _contexto; // DbContext para acceder a la base de datos

        public PublicacionController(ApplicationDbContext contexto)
        {
            _contexto = contexto; // Inicializa el DbContext en el constructor
        }

        [HttpGet("pendientes")] // Define una ruta para obtener publicaciones pendientes
        public IActionResult ObtenerPublicacionesPendientes()
        {
            // Obtiene las publicaciones pendientes de la base de datos y las convierte en un formato espec�fico
            var publicacionesPendientes = _contexto.Publicaciones
                .Where(p => p.PendienteAprobacion)
                .Select(p => new
                {
                    p.Id,
                    NombreAutor = p.Autor.Nombre,
                    FechaEnvio = p.FechaEnvio
                })
                .ToList();

            // Devuelve las publicaciones pendientes en formato JSON
            return Ok(publicacionesPendientes);
        }

        [HttpPost("aprobar")] // Define una ruta para aprobar una publicaci�n
        public IActionResult AprobarPublicacion([FromBody] int idPublicacion)
        {
            // Busca la publicaci�n en la base de datos por su ID
            var publicacion = _contexto.Publicaciones.Find(idPublicacion);
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicaci�n no se encuentra
            }

            // Actualiza el estado de aprobaci�n de la publicaci�n y guarda los cambios en la base de datos
            publicacion.PendienteAprobacion = false;
            _contexto.SaveChanges();

            // Devuelve una respuesta OK
            return Ok();
        }

        [HttpPost("rechazar")] // Define una ruta para rechazar una publicaci�n
        public IActionResult RechazarPublicacion([FromBody] int idPublicacion)
        {
            // Busca la publicaci�n en la base de datos por su ID
            var publicacion = _contexto.Publicaciones.Find(idPublicacion);
            if (publicacion == null)
            {
                return NotFound(); // Devuelve un error 404 si la publicaci�n no se encuentra
            }

            // Elimina la publicaci�n de la base de datos y guarda los cambios
            _contexto.Publicaciones.Remove(publicacion);
            _contexto.SaveChanges();

            // Devuelve una respuesta OK
            return Ok();
        }
    }
}
