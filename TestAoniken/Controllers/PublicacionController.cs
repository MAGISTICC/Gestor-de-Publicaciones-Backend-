using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TestAoniken.Models;

namespace TestAoniken.Controllers
{
    [Route("api/publicaciones")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly ApplicationDbContext _contexto;

        public PublicacionController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("pendientes")]
        public IActionResult ObtenerPublicacionesPendientes()
        {
            var publicacionesPendientes = _contexto.Publicaciones
                .Where(p => p.PendienteAprobacion)
                .Select(p => new
                {
                    p.Id,
                    NombreAutor = p.Autor.Nombre,
                    FechaEnvio = p.FechaEnvio
                })
                .ToList();

            return Ok(publicacionesPendientes);
        }

        [HttpPost("aprobar")]
        public IActionResult AprobarPublicacion([FromBody] int idPublicacion)
        {
            var publicacion = _contexto.Publicaciones.Find(idPublicacion);
            if (publicacion == null)
            {
                return NotFound();
            }

            publicacion.PendienteAprobacion = false;
            _contexto.SaveChanges();

            return Ok();
        }

        [HttpPost("rechazar")]
        public IActionResult RechazarPublicacion([FromBody] int idPublicacion)
        {
            var publicacion = _contexto.Publicaciones.Find(idPublicacion);
            if (publicacion == null)
            {
                return NotFound();
            }

            _contexto.Publicaciones.Remove(publicacion);
            _contexto.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] Publicacion publicacionActualizada)
        {
            var publicacion = _contexto.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            publicacion.Titulo = publicacionActualizada.Titulo;
            publicacion.Contenido = publicacionActualizada.Contenido;
            // Actualiza otras propiedades según sea necesario

            _contexto.SaveChanges();

            return Ok(publicacion);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPublicacion(int id)
        {
            var publicacion = _contexto.Publicaciones.Find(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            _contexto.Publicaciones.Remove(publicacion);
            _contexto.SaveChanges();

            return Ok();
        }
    }
}
