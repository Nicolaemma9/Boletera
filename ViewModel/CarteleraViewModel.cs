using Boletera.Model;
using Boletera.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.ViewModel
{
    public class CarteleraViewModel : ViewModelBase
    {
        private IPeliculaRepository peliculaRepository;

        public ObservableCollection<PeliculaModel> Peliculas { get; set; }

        private PeliculaModel _peliculaSeleccionada;
        public PeliculaModel PeliculaSeleccionada
        {
            get { return _peliculaSeleccionada; }
            set
            {
                _peliculaSeleccionada = value;
                OnProperyChanged(nameof(PeliculaSeleccionada));
            }
        }

        private string _usuarioLogueado;
        public string UsuarioLogueado
        {
            get { return _usuarioLogueado; }
            set
            {
                _usuarioLogueado = value;
                OnProperyChanged(nameof(UsuarioLogueado));
            }
        }

        public CarteleraViewModel(string username)
        {
            UsuarioLogueado = username;

            peliculaRepository = new PeliculaRepository();
            Peliculas = new ObservableCollection<PeliculaModel>();

            CargarPeliculas();
        }

        private void CargarPeliculas()
        {
            var lista = peliculaRepository.GetAllPeliculas();

            Peliculas.Clear();
            foreach (var pelicula in lista)
            {
                Peliculas.Add(pelicula);
            }

            OnProperyChanged(nameof(Peliculas));
        }
    }
}
