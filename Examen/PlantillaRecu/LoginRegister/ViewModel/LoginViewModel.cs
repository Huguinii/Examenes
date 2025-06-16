using CommunityToolkit.Mvvm.Input;
using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Helpers;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace LoginRegister.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IHttpJsonProvider<UserDTO> _httpJsonProvider;
        private readonly IHttpJsonProvider<UsuarioDTO> _usuarioService;




        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _passwordView;

        public LoginViewModel(IHttpJsonProvider<UserDTO> httpJsonProvider, IHttpJsonProvider<UsuarioDTO> usuarioService)
        {
            _httpJsonProvider = httpJsonProvider;
            _usuarioService = usuarioService;
        }

        [RelayCommand]
        public async Task Login()
        {

            App.Current.Services.GetService<LoginDTO>().UserName = Name;
            App.Current.Services.GetService<LoginDTO>().Password = PasswordView;
           




            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(PasswordView))
            {
                MessageBox.Show("Por favor, rellene ambos campos.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
               
                UserDTO user = await _httpJsonProvider.LoginPostAsync(Constants.LOGIN_PATH, App.Current.Services.GetService<LoginDTO>());

                if (user != null && user.IsSuccess)
                {
                    await CrearOObtenerUsuarioDesdeLoginAsync();
                    App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().ComentariosViewModel;
                    App.Current.Services.GetService<MainViewModel>().LoadAsync();
                    Name = string.Empty;
                    PasswordView = string.Empty;
                 
                }
                else
                {
                  
                    MessageBox.Show("Credenciales incorrectas. Intente de nuevo.", "Error de Inicio de Sesión", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Ocurrió un error durante el inicio de sesión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        [RelayCommand]
        private async void Register()
        {

            var mainWindow = App.Current.Services.GetService<MainViewModel>();
            mainWindow.SelectedViewModel = App.Current.Services.GetService<MainViewModel>().RegistroViewModel;
        }

        public async Task<UsuarioDTO?> CrearOObtenerUsuarioDesdeLoginAsync()
        {
            var loginDTO = App.Current.Services.GetService<LoginDTO>();

            var nuevoUsuario = new UsuarioDTO
            {
                UserName = loginDTO.UserName,
                Email = "",
                Password = loginDTO.Password,
                Rol = "DAM_ORD_DOS"
            };
            

            var listaUsuarios = await _usuarioService.GetAsync(Constants.USUARIO_URL);
            if (listaUsuarios.Count()==0)
            {
                var usuarioCreado = await _usuarioService.PostAsync(Constants.USUARIO_URL, nuevoUsuario);

                if (usuarioCreado != null)
                {
                    var usuarioSingleton = App.Current.Services.GetService<UsuarioDTO>();
                    usuarioSingleton.Id = usuarioCreado.Id;
                    usuarioSingleton.UserName = usuarioCreado.UserName;
                    usuarioSingleton.Email = usuarioCreado.Email;
                    usuarioSingleton.Password = usuarioCreado.Password;
                    usuarioSingleton.Rol = usuarioCreado.Rol;

                    return usuarioCreado;
                }
            }
            foreach (var usuario in listaUsuarios)
            {
                if (usuario.UserName == nuevoUsuario.UserName && usuario.Password == nuevoUsuario.Password)
                {
                    var usuarioSingleton = App.Current.Services.GetService<UsuarioDTO>();
                    usuarioSingleton.Id = usuario.Id;
                    usuarioSingleton.UserName = usuario.UserName;
                    usuarioSingleton.Email = usuario.Email;
                    usuarioSingleton.Password = usuario.Password;
                    usuarioSingleton.Rol = usuario.Rol;

                    return usuario;
                }
                else
                {
                    var usuarioCreado = await _usuarioService.PostAsync(Constants.USUARIO_URL, nuevoUsuario);

                    if (usuarioCreado != null)
                    {
                        var usuarioSingleton = App.Current.Services.GetService<UsuarioDTO>();
                        usuarioSingleton.Id = usuarioCreado.Id;
                        usuarioSingleton.UserName = usuarioCreado.UserName;
                        usuarioSingleton.Email = usuarioCreado.Email;
                        usuarioSingleton.Password = usuarioCreado.Password;
                        usuarioSingleton.Rol = usuarioCreado.Rol;

                        return usuarioCreado;
                    }
                }
            }

            return null;
        }


    }
}

