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
    /// Lógica de interacción para ManejoUsuariosView.xaml
    /// </summary>
    public partial class ManejoUsuariosView : Window
    {
        public ManejoUsuariosView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Crear nueva ventana
            var Login = new Boletera.Views.LoginView();
            // Asignarla como ventana principal
            Application.Current.MainWindow = Login;
            // Mostrarla
            Login.Show();
            // Cerrar la anterior (login)
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Login)
                {
                    window.Close();
                    break;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Crear nueva ventana
            var Login = new Boletera.Views.ManejoPeliculasView();
            // Asignarla como ventana principal
            Application.Current.MainWindow = Login;
            // Mostrarla
            Login.Show();
            // Cerrar la anterior (login)
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Login)
                {
                    window.Close();
                    break;
                }
            }
        }
    }

}
