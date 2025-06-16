using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.ComentarioDTO;

public class CreateComentarioDTO
{
    public string Comentario {  get; set; }
    public string Tema { get; set; }
    public DateTime FechaDePublicacion { get; set; }
    public string UsuarioId { get; set; }
    

}
