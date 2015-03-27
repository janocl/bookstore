using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BLL
{
    public class EditorialesBL
    {
        // Crea una nueva editorial.
        public Editorial Create(Editorial newEditorial)
        {
            using (var r = new Repository<Editorial>())
            {
                newEditorial = r.Create(newEditorial);
            }

            return newEditorial;
        }

        // Devuelve una editorial segun el ID solicitado.
        public Editorial RetrievedById(short ID) 
        {
            Editorial Result = null;

            using (var r = new Repository<Editorial>())
            {
                Result = r.Retrieve( p => p.IDEditorial == ID);
            }

            return Result;
        }

        // Devuelve una lista de todas las Editoriales.
        public List<Editorial> GetAllEditoriales()
        {

            List<Editorial> Result = null;
            using (var r = new Repository<Editorial>())
            {
                Result = r.SelectQuery(e => new Editorial()
                { 
                    IDEditorial = e.IDEditorial,
                    Nombre = e.Nombre,
                    Direccion = e.Direccion,
                    Telefono = e.Telefono,
                    E_mail = e.E_mail,
                    Web = e.Web,
                    Libros = e.Libros
                }).ToList();
            }

            return Result;

        }


    }
}
