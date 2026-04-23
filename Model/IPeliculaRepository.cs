using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Model
{
    public interface IPeliculaRepository
    {
        void Add(PeliculaModel peliculaModel);

        void Update(PeliculaModel peliculaModel);

        void Delete(PeliculaModel peliculaModel);

        IEnumerable<PeliculaModel> GetAllPeliculas();

    }
}
