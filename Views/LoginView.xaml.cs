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
using System.Windows.Shapes;

namespace Boletera.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Crear nueva ventana
            var RegistroView = new RegistroView();
            // Asignarla como ventana principal
            Application.Current.MainWindow = RegistroView;
            // Mostrarla
            RegistroView.Show();
            // Cerrar la anterior (login)
            foreach (Window window in Application.Current.Windows)
            {
                if (window != RegistroView)
                {
                    window.Close();
                    break;
                }


            }
        }
    }

}
