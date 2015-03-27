using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DetalleBL
    {
        private List<Detalle> lista = null;
        private LibrosBL libro = null;
        private EditorialesBL editorial = null;

        public DetalleBL()
        {
            libro = new LibrosBL();
            editorial = new EditorialesBL();
            lista = new List<Detalle>();
        }

        public List<Detalle> getDetalle()
        {

            lista.Add(new Detalle() 
            { 
                ListaLibros = libro.GetAllBooks().ToList(),
                ListaEditoriales = editorial.GetAllEditoriales().ToList()
            });

            return lista;

        }

    }
}
