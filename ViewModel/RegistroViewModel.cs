using Boletera.Model;
using Boletera.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions; //se supone que se usara para verificar la autenticidad del correo electronico
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boletera.ViewModel
{
    public class RegistroViewModel : ViewModelBase
    {
        private readonly RepositoryBase repositoryBase;

        private ObservableCollection<UserModel> _users;
        private UserModel _user;
        private UserModel _email;
        private IUserRepository userRepository;



        public UserModel Email
        {
            get => _email;
            set {
                _email = value;
                OnProperyChanged(nameof(Email));
            }
        }
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnProperyChanged(nameof(User));
            }
        }

        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnProperyChanged(nameof(Users));
                }
            }
        }

        public RegistroViewModel()
        {
            userRepository = new UserRepository();
            _user = new UserModel();
        }

        public ICommand AddCommand
        {
            get
            {
                return new ViewModelCommand(AddExecute,
                AddCanExecute);
            }
        }

        //metodo que agregue para verificar que si es un correo electronico
        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }


        private void AddExecute(object user)
        {

            if (!EsCorreoValido(User.Email))
            {
                MessageBox.Show("Ingresa un Email valido.",
                                "Campo invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existeEmail = userRepository.EmailExiste(User.Email);
            if (existeEmail)
            {
                MessageBox.Show("El Email que se intenta registrar ya existe en otro usuario, Por favor, usa un email nuevo para cuentas nuevas.",
                                "Email duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User?.UserName) ||
                string.IsNullOrWhiteSpace(User.Name) || string.IsNullOrWhiteSpace(User.LastName) ||
                string.IsNullOrWhiteSpace(User.Email))
            {
                MessageBox.Show("Por favor, completa todos los campos antes de guardar.",
                                "Campos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que las contraseñas coincidan
            if (User.Password != User.ConfirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, verifica.",
                                "Error de contraseña", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar si el username ya existe usando GetByUsername()
            var existingUser = userRepository.GetByUername(User.UserName);
            if (existingUser != null)
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor, elige otro.",
                                "Usuario duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Si pasa las validaciones, se añade el usuario
            User.Id = Guid.NewGuid().ToString();
            userRepository.Add(User);
            MessageBox.Show("Usuario añadido correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private bool AddCanExecute(object user)
        {
            // Deshabilita el botón si los campos están vacíos
            return !string.IsNullOrWhiteSpace(User?.UserName) && !string.IsNullOrWhiteSpace(User?.Password) &&
                   !string.IsNullOrWhiteSpace(User?.Name) && !string.IsNullOrWhiteSpace(User?.LastName) &&
                   !string.IsNullOrWhiteSpace(User?.Email);
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ViewModelCommand(DeleteExecute, DeleteCanExecute);
            }
        }

        private void DeleteExecute(Object user)
        {
            userRepository.Delete(User);
            // Borra el usuario usando el Id
            // Actualizar la lista de usuarios si es necesario
            // Users = userRepository.Get();
        }

        private bool DeleteCanExecute(Object user)
        {
            // Verifica que el objeto user no sea nulo y tenga un Id válido
            return true;
        }

        public ICommand EditCommand
        {
            get
            {
                return new ViewModelCommand(EditExecute, EditCanExecute);
            }
        }
        private void EditExecute(Object user)
        {
            userRepository.Update(User); // Borra el usuario usando el Id
                                         // Actualizar la lista de usuarios si es necesario
                                         // Users = userRepository.Get();
        }

        private bool EditCanExecute(Object user)
        {
            // Verifica que el objeto user no sea nulo y tenga un Id válido
            return true;
        }



    }
}