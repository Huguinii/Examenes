using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Helpers;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace LoginRegister.ViewModel
{
    public partial class ComentariosViewModel : ViewModelBase
    {
        private readonly UsuarioDTO _usuarioAutenticado;
        private readonly IHttpJsonProvider<ComentarioDTO> _comentariosService;

        public ObservableCollection<ComentarioDTO> Comentarios { get; } = new();

        public ComentariosViewModel(IHttpJsonProvider<ComentarioDTO> comentariosService, UsuarioDTO usuarioAutenticado)
        {
            _comentariosService = comentariosService;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public int ComentariosCount => Comentarios.Count;

        [ObservableProperty]
        private string tema;

        [ObservableProperty]
        private string textoComentario;

        [ObservableProperty]
        private string temaError;

        [ObservableProperty]
        private string comentarioError;

        [ObservableProperty]
        private ComentarioDTO comentarioEditando;

        public override async Task LoadAsync()
        {
            var lista = await _comentariosService.GetAsync(Helpers.Constants.COMENTARIO_URL);
            if (lista != null)
            {
                Comentarios.Clear();
                foreach (var c in lista)
                {
                    Comentarios.Add(c);
                }
                OnPropertyChanged(nameof(ComentariosCount));
            }
        }


        [RelayCommand]
        private async Task EnviarComentarioAsync()
        {
            TemaError = null;
            ComentarioError = null;

            bool esValido = true;

            if (string.IsNullOrWhiteSpace(Tema))
            {
                TemaError = "El tema no puede estar vacío.";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(TextoComentario))
            {
                ComentarioError = "El comentario no puede estar vacío.";
                esValido = false;
            }
            else if (TextoComentario.Length > 140)
            {
                ComentarioError = "El comentario no puede exceder los 140 caracteres.";
                esValido = false;
            }

            if (!esValido) return;

            var nuevoComentario = new ComentarioDTO
            {
                Tema = Tema.Trim(),
                Comentario = TextoComentario.Trim(),
                FechaDePublicacion = DateTime.Now,
                UsuarioId = _usuarioAutenticado.Id
            };

            var resultado = await _comentariosService.PostAsync(Helpers.Constants.COMENTARIO_URL, nuevoComentario);

            if (resultado != null)
            {
                Comentarios.Add(resultado);
                OnPropertyChanged(nameof(ComentariosCount));

                Tema = string.Empty;
                TextoComentario = string.Empty;
            }
        }
        [RelayCommand]
        public async Task EliminarComentario(ComentarioDTO comentario)
        {
            if (comentario == null || comentario.UsuarioId != _usuarioAutenticado.Id)
                return;

            var result = MessageBox.Show($"¿Seguro que quieres eliminar el comentario \"{comentario.Tema}\"?",
                                        "Confirmar eliminación",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Eliminar el comentario seleccionado
                var eliminado = await _comentariosService.Delete(Helpers.Constants.COMENTARIO_URL, comentario.Id);
                if (eliminado != null)
                {
                    // Encontrar índice del comentario para eliminar el siguiente también
                    var index = Comentarios.IndexOf(comentario);
                    if (index >= 0)
                    {
                        Comentarios.RemoveAt(index);

                        // Si hay comentario siguiente, eliminarlo también
                        if (index < Comentarios.Count)
                        {
                            var siguienteComentario = Comentarios[index];

                            // Solo eliminar el siguiente si es del mismo usuario autenticado, para evitar errores de permisos
                            if (siguienteComentario.UsuarioId == _usuarioAutenticado.Id)
                            {
                                var eliminadoSiguiente = await _comentariosService.Delete(Helpers.Constants.COMENTARIO_URL, siguienteComentario.Id);
                                if (eliminadoSiguiente != null)
                                {
                                    Comentarios.RemoveAt(index);
                                }
                                else
                                {
                                    MessageBox.Show("Error al eliminar el comentario siguiente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }

                    OnPropertyChanged(nameof(ComentariosCount));
                }
                else
                {
                    MessageBox.Show("Error al eliminar el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        [RelayCommand]
        public async Task EditarComentarioAsync(ComentarioDTO comentario)
        {
            if (comentario == null || comentario.UsuarioId != _usuarioAutenticado.Id)
                return;

            // Aquí llamamos al PUT para actualizar en backend
            var actualizado = await _comentariosService.PutAsync($"{Helpers.Constants.COMENTARIO_URL}{comentario.Id}/", comentario);

            if (actualizado != null)
            {
                // Actualiza el comentario en la lista para refrescar UI si es necesario
                int index = Comentarios.IndexOf(comentario);
                if (index >= 0)
                {
                    Comentarios[index] = actualizado;
                }
                ComentarioEditando = null; // Salimos del modo edición
            }
            else
            {
                MessageBox.Show("Error al actualizar el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

}

