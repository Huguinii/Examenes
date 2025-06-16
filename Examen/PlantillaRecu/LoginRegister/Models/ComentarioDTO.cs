using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LoginRegister.Models
{
    public class ComentarioDTO : ObservableObject
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("tema")]
        public string Tema { get; set; }

        [JsonPropertyName("comentario")]
        public string Comentario { get; set; }

        [JsonPropertyName("fechaDePublicacion")]
        public DateTime FechaDePublicacion { get; set; }

        [JsonPropertyName("usuarioId")]
        public string UsuarioId { get; set; }

        [JsonIgnore]
        private bool isOwnedByCurrentUser;
        [JsonIgnore]
        public bool IsOwnedByCurrentUser
        {
            get => isOwnedByCurrentUser;
            set => SetProperty(ref isOwnedByCurrentUser, value);
        }
    }
}
