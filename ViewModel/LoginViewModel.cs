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

        //intentar hacer funcionar con emal
        private string _email; //email

        
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
                new System.Net.NetworkCredential(Username, Password));//antes Username
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null); //antes Username
                // Crear nueva ventana
                var manejadorView = new CarteleraView();
                // Asignarla como ventana principal
                Application.Current.MainWindow = manejadorView;
                // Mostrarla
                manejadorView.Show();
                // Cerrar la anterior (login)
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != manejadorView)
                    {
                        window.Close();
                        break;
                    }


                }
            }
            else
            {
                ErrorMessage = "*Usuario o contraseña invalidos";
            }
        }
    }
}
