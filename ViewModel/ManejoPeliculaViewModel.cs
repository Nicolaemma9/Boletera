using Boletera.Model;
using Boletera.Repositories;
using Boletera.Views;
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
    public class ManejoPeliculaViewModel : ViewModelBase
    {
        private IPeliculaRepository peliculaRepository;
        private ObservableCollection<PeliculaModel> _pelicula;
        private PeliculaModel _peliculaSeleccionada;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }


        public ObservableCollection<PeliculaModel> Pelicula
        {
            get { return _pelicula; }
            set
            {
                _pelicula = value;
                OnProperyChanged(nameof(Pelicula));
            }
        }
        public PeliculaModel PeliculaSellecionada
        {
            get { return _peliculaSeleccionada; }
            set
            {
                _peliculaSeleccionada = value;
                OnProperyChanged(nameof(PeliculaSellecionada));
            }
        }
        public ManejoPeliculaViewModel()
        {
            peliculaRepository = new PeliculaRepository();
            CargarPelicula();
            AddCommand = new ViewModelCommand(EjecutarAgregarPelicula);
            EditCommand = new ViewModelCommand(EjecutarEditarPelicula, PuedeEjecutarPeliculaAccion);
            DeleteCommand = new ViewModelCommand(EjecutarBorrarPelicula, PuedeEjecutarPeliculaAccion);
            RefreshCommand = new ViewModelCommand(o => CargarPelicula());
        }



        private void CargarPelicula()
        {
            try
            {
                // Usamos una nueva lista de peliculas obtenida de la base de datos
                Pelicula = new ObservableCollection<PeliculaModel>(peliculaRepository.GetAllPeliculas());
            }
            catch
            {
                MessageBox.Show("Error al cargar los usuarios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //metodos auxiliares

        private void EjecutarAgregarPelicula(object obj)
        {
            // Crear nueva ventana
            var registrarPeliculaView = new Boletera.Views.RegistrarPeliculaView();
            // Asignarla como ventana principal
            Application.Current.MainWindow = registrarPeliculaView;
            // Mostrarla
            registrarPeliculaView.Show();
        }
        private void EjecutarEditarPelicula(object obj)
        {
            var pelicula = obj as PeliculaModel;
            if (pelicula == null)
            {
                MessageBox.Show("Seleccione una pelicula válida.");
                return;
            }
            peliculaRepository.Update(pelicula);
            MessageBox.Show("Pelicula actualizadoa correctamente.");
            CargarPelicula();
        }
        private void EjecutarBorrarPelicula(object obj)
        {
            try
            {
                if (PeliculaSellecionada == null)
                {
                    MessageBox.Show("Seleccione una pelicula para eliminar.", "Advertencia",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"¿Desea eliminar al usuario {PeliculaSellecionada.Nombre}?",
                    "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    peliculaRepository.Delete(PeliculaSellecionada);
                    MessageBox.Show("Pelicula eliminadoa.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPelicula();
                }
            }
            catch
            {
                MessageBox.Show("Error al eliminar La Pelicula. ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool PuedeEjecutarPeliculaAccion(object obj)
        {
            return PeliculaSellecionada != null;
        }
    }
}