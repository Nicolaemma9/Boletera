using Boletera.Model;
using Boletera.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Boletera.ViewModel
{
    public class RegistrarPeliculaViewModel : ViewModelBase
    {
        private readonly IPeliculaRepository peliculaRepository;

        private PeliculaModel _pelicula;
        private PeliculaModel _nombre;

        public PeliculaModel Nombre
        {
            get => _nombre; 
            set
            {
                _nombre = value;
                OnProperyChanged(nameof(Nombre));
            }
        }



        public PeliculaModel Pelicula
        {
            get { return _pelicula; }
            set
            {
                _pelicula = value;
                OnProperyChanged(nameof(Pelicula));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnProperyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand AddCommand { get; }

        public RegistrarPeliculaViewModel()
        {
            peliculaRepository = new PeliculaRepository();

            Pelicula = new PeliculaModel();

            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
        }

        private bool CanExecuteAddCommand(object obj)
        {


            if (Pelicula == null)
                return false;

            if (string.IsNullOrWhiteSpace(Pelicula.Nombre))
                return false;

            //if (Pelicula.Precio <= 0) //esto se vuelve inutil desde el momento en que verificamos el precion con PrecioTexto y no con Precio
                //return false;
            if (string.IsNullOrWhiteSpace(Pelicula.PrecioTexto))
                return false;

            if (string.IsNullOrWhiteSpace(Pelicula.Sala))
                return false;

            if (string.IsNullOrWhiteSpace(Pelicula.Idioma))
                return false;

            if (string.IsNullOrWhiteSpace(Pelicula.Subtitulos))
                return false;

            if (string.IsNullOrWhiteSpace(Pelicula.Horarios))
                return false;

            return true;
        }

        private void ExecuteAddCommand(object obj)
        {
            if (!decimal.TryParse(Pelicula.PrecioTexto, out decimal precio))
            {
                MessageBox.Show("Procura que lo que ingreses, sea un numero valido (o minimamente un numero)",
                                "Precio inválido",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que 0.",
                                "Precio inválido",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            Pelicula.Precio = precio;

            try
            {
                peliculaRepository.Add(Pelicula);

                MessageBox.Show("Película registrada correctamente.", "Éxito",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpiar campos
                Pelicula = new PeliculaModel();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error al registrar película.";
                MessageBox.Show(ex.Message);
            }
        }
    }
}
