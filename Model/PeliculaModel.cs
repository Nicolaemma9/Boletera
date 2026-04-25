using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Model
{
    public class PeliculaModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Sala { get; set; }

        public string Idioma { get; set; }

        public string Subtitulos { get; set; }

        public string Horarios { get; set; }

    }
}
