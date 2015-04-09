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
        private readonly IRepository<Editorial> irepo;

        public EditorialesBL()
            : this(new Repository<Editorial>())
        { }

        public EditorialesBL(IRepository<Editorial> repository)
        {
            irepo = repository;
        }

        // Crea una nueva editorial.
        public Editorial Create(Editorial newEditorial)
        {

            try
            {
                newEditorial = irepo.Create(newEditorial);
            }
            catch (Exception)
            {
                throw;
            }

            return newEditorial;
        }

        // Devuelve una editorial segun el ID solicitado.
        public Editorial RetrievedById(short ID) 
        {
            Editorial Result = null;

            try
            {
                Result = irepo.Retrieve(p => p.IDEditorial == ID);
            }
            catch (Exception)
            {
                throw;
            }

            return Result;
        }

        // Devuelve una lista de todas las Editoriales.
        public List<Editorial> GetAllEditoriales()
        {

            List<Editorial> Result = null;

            try
            {
                Result = irepo.SelectQuery(e => new Editorial()
                {
                    IDEditorial = e.IDEditorial,
                    Nombre = e.Nombre,
                    Direccion = e.Direccion,
                    Telefono = e.Telefono,
                    E_mail = e.E_mail,
                    Web = e.Web,
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return Result;

        }


    }
}
