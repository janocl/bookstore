using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Detalle
    {
        public List<Libro> ListaLibros { get; set; }
        public List<Editorial> ListaEditoriales { get; set; }
    }
}
