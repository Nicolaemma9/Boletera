using Boletera.Model;
using Boletera.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boletera.ViewModel
{
    public class ManejoUsuariosViewModel : ViewModelBase
    {
        private IUserRepository userRepository;
        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnProperyChanged(nameof(Users));
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnProperyChanged(nameof(SelectedUser));
            }
        }
        // Constructor
        public ManejoUsuariosViewModel()
        {
            userRepository = new UserRepository();
            LoadUsers();
            AddCommand = new ViewModelCommand(ExecuteAddUser);
            EditCommand = new ViewModelCommand(ExecuteEditUser, CanExecuteUserAction);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteUser, CanExecuteUserAction);
            RefreshCommand = new ViewModelCommand(o => LoadUsers());
        }

        private void LoadUsers()
        {
            try
            {
                // Usamos una nueva lista de usuarios obtenida de la base de datos
                Users = new ObservableCollection<UserModel>(userRepository.GetAllUsers());
            }
            catch
            {
                MessageBox.Show("Error al cargar los usuarios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Métodos auxiliares

        private void ExecuteAddUser(object obj)
        {

            // Crear nueva ventana
            var registroView = new Boletera.Views.RegistroView();
            // Asignarla como ventana principal
            Application.Current.MainWindow = registroView;
            // Mostrarla
            registroView.Show();
        }

        private void ExecuteEditUser(object obj)
        {
            var user = obj as UserModel;
            if (user == null)
            {
                MessageBox.Show("Seleccione un usuario válido.");
                return;
            }
            userRepository.Update(user);
            MessageBox.Show("Usuario actualizado correctamente.");
            LoadUsers();
        }
        private void ExecuteDeleteUser(object obj)
        {
            try
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"¿Desea eliminar al usuario {SelectedUser.UserName}?",
                    "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    userRepository.Delete(SelectedUser);
                    MessageBox.Show("Usuario eliminado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                }
            }
            catch
            {
                MessageBox.Show("Error al eliminar el usuario. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CanExecuteUserAction(object obj)
        {
            return SelectedUser != null && SelectedUser.UserName.ToLower() != "admin";
        }



    }
}
