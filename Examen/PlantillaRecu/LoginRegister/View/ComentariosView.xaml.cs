using LoginRegister.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace LoginRegister.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>

    public partial class ComentariosView : UserControl
    {
 
        public ComentariosView()
        {
            InitializeComponent();
         
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComentarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = DataContext as ComentariosViewModel;
                var textBox = sender as System.Windows.Controls.TextBox;
                if (vm != null && textBox != null && vm.ComentarioEditando != null)
                {
                    _ = vm.EditarComentarioAsync(vm.ComentarioEditando);
                    Keyboard.ClearFocus(); // Para salir del TextBox y quitar modo edición
                }
            }
        }

    }
}

