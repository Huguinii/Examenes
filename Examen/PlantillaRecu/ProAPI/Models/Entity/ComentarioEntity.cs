using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestAPI.Models.Entity
{
    public class ComentarioEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(140), NotNull]
        public string Comentario { get; set; }

        [Required, NotNull]
        public string Tema { get; set; }

        public DateTime FechaDePublicacion { get; set; } = DateTime.Now;

        [Required]
        public int UsuarioId { get; set; }
    }
}
