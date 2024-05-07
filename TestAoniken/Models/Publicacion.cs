using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAoniken.Models
{
    public class Publicacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Contenido { get; set; }

        public int IdAutor { get; set; }

        [ForeignKey("IdAutor")]
        public Usuario Autor { get; set; }

        public DateTime FechaEnvio { get; set; }

        public bool PendienteAprobacion { get; set; }
    }
}
