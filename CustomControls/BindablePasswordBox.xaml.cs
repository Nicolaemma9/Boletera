using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Boletera.CustomControls
{
    /// <summary>
    /// Lógica de interacción para BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public BindablePasswordBox()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPasswordchanged;
        }
        public static readonly DependencyProperty
            PasswordProperty = DependencyProperty.Register(
                "Password", typeof(SecureString),
                typeof(BindablePasswordBox));
        public SecureString Password
        {
            get
            {
                return (SecureString)GetValue(
                  PasswordProperty);
            }
            set { SetValue(PasswordProperty, value); }
        }

        private void OnPasswordchanged(object sender,
            RoutedEventArgs e)
        {
            Password = txtPassword.SecurePassword;
        }
    }
}