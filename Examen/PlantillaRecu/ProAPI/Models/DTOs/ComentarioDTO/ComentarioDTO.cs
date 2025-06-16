using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.ComentarioDTO;

public class ComentarioDTO : CreateComentarioDTO
{
    public Guid Id { get; set; }

}
