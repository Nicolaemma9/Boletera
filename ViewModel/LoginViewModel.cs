using Boletera.Model;
using Boletera.Repositories;
using Boletera.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boletera.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username; //usuario
        private SecureString _password; // contrase;a
        private string _errorMessage; //guarda el mensaje de erro en caso de que el login falle
        private bool _isViewVisible = true;
        private IUserRepository _userRepository;
        private string _email;//Prueba de email

        //intentar hacer funcionar con emal

        
        public string Email
        {

            get { return _email; }
            set 
            {
                _email = value;
                OnProperyChanged(nameof(Email));
            }
        }
        //intentar hacer funcionar con emal

        //propiedades
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnProperyChanged(nameof(Username));
            }

        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnProperyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnProperyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnProperyChanged(nameof(IsViewVisible));
            }
        }

        // Comandos
        //LoginCommand se ejecuta cuadno el usuario hace click en login
        public ICommand LoginCommand { get; }

        //mostrar la contrase;a
        public ICommand ShowPasswordCommand { get; }

        // constructor
        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(

                ExecuteLoginCommand,
                CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) //antes Username
                || Username.Length < 3 //antes Username
                || Password == null
                || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        public void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _userRepository.AuthenticateUser(
                new System.Net.NetworkCredential(Username, Password));

            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);

                Window nuevaVentana;

                // SI ES ADMIN
                if (Username.ToLower() == "admin") 
                {
                    nuevaVentana = new Boletera.Views.ManejoUsuariosView();
                }
                // SI ES USUARIO NORMAL
                else
                {
                    nuevaVentana = new CarteleraView();
                    nuevaVentana.DataContext = new CarteleraViewModel(Username);
                }

                Application.Current.MainWindow = nuevaVentana;
                nuevaVentana.Show();

                // Cerrar ventana anterior (Login)
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != nuevaVentana)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                ErrorMessage = "*Usuario o contraseña inválidos";
            }
        }
    }
}
