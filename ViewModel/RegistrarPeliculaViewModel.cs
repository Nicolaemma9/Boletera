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

            if (Pelicula.Precio <= 0)
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
