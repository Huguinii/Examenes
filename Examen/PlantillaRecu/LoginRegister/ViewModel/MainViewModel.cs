using CommunityToolkit.Mvvm.Input;
using System.Windows;


namespace LoginRegister.ViewModel;

public partial class MainViewModel : ViewModelBase
{
    private ViewModelBase? _selectedViewModel;
    
    public MainViewModel(LoginViewModel loginViewModel, RegistroViewModel registroViewModel, ComentariosViewModel comentariosViewModel)
    {
        LoginViewModel = loginViewModel;
        RegistroViewModel = registroViewModel;
        ComentariosViewModel = comentariosViewModel;
        _selectedViewModel = loginViewModel;

    }
    
    
    public ViewModelBase? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            SetProperty(ref _selectedViewModel, value);
            
        }
    }

    public LoginViewModel LoginViewModel { get; }
    public RegistroViewModel RegistroViewModel { get; }
    public ComentariosViewModel ComentariosViewModel { get; }


    public override async Task LoadAsync()
    {
        if (SelectedViewModel is not null)
        {
            await SelectedViewModel.LoadAsync();
        }
    }

    [RelayCommand]
    private async Task SelectViewModelAsync(object? parameter)
    {
        if (parameter is ViewModelBase viewModel)
        {
            SelectedViewModel = viewModel;
            await LoadAsync();
        }
    }

    

}




