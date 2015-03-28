using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class LibrosBL
    {

        private IRepository<Libro> _repo;

        public LibrosBL() { }
        public LibrosBL(IRepository<Libro> repo)
        {
            this._repo = repo;
        }

        // Crea un nuevo libro.
        public Libro Create(Libro newLibro)
        {
            Libro Result = null;
            using (var r = new Repository<Libro>())
            {
                Libro libBuscado = r.Retrieve(p => p.Titulo == newLibro.Titulo);

                // Si no existe entonces lo creamos.
                if (libBuscado == null)
                {
                    Result = r.Create(newLibro);
                }
                else
                {
                    throw (new Exception("El Titulo de Libro especificado ya existe..."));
                }
            }

            return Result;
        }



        // Actualiza los registros de un libro.
        public bool Update(Libro libroToUpdate)
        {
            bool Result = false;
            using (var r = new Repository<Libro>())
            {
                Libro libBuscado = r.Retrieve(p => p.Titulo == libroToUpdate.Titulo);

                // Si no existe entonces lo creamos.
                if (libBuscado == null)
                {
                    Result = r.Update(libroToUpdate);
                }
                else
                {
                    throw (new Exception("El Titulo de Libro especificado ya existe"));
                }
            }

            return Result;
        }



        // Elimina un libro y no se puede eliminar un libro que tenga existencia
        public bool Delete(short ID)
        {
            bool Result = false;

            var Libro = RetrieveById(ID);
            if (Libro != null)
            {
                if (Libro.Stock == 0)
                {
                    using (var r = new Repository<Libro>())
                    {
                        Result = r.Delete(Libro);
                    }
                }
                else
                {
                    throw (new Exception("El Libro no puede ser eliminado porque esta con existencia..."));
                }
            }
            else
            {
                throw (new Exception("El Libro especificado no existe..."));
            }

            return Result;
        }



        // Devuelve una lista de todos los libros disponibles.
        public List<Libro> GetAllBooks()
        {
            List<Libro> Result = null;
            using (var r = _repo)
            {
                Result = _repo.SelectQuery(p => new Libro()
                {
                    IDLibro = p.IDLibro,
                    ISBN = p.ISBN,
                    Titulo = p.Titulo,
                    Autor = p.Autor,
                    Paginas = p.Paginas,
                    Precio = p.Precio,
                    Publicacion = p.Publicacion,
                    Stock = p.Stock,
                    Descripcion = p.Descripcion
                }).ToList();
            }

            return Result;
        }



        // Devuelve un lista de libros por su ID
        public Libro RetrieveById(short ID)
        {
            Libro Result = null;

            using (var r = new Repository<Libro>())
            {
                Result = r.Retrieve(p => p.IDLibro == ID);
            }

            return Result;
        }



        // Devuelve una lista de libros de acuerdo al titulo del libro.
        public List<Libro> FilterBookByTitulo(string titulo)
        {
            List<Libro> Result = null;
            using (var r = new Repository<Libro>())
            {
                Result = r.Filter(p => p.Titulo.Contains(titulo));
            }

            return Result;
        }



        // Devuelve una lista de Libros por el ID de la Editorial, siempre que el libro exista.
        public List<Libro> FilterByEditorialID(short IDEditorial)
        {
            List<Libro> Result = null;
            using (var r = new Repository<Libro>())
            {
                Result = r.Filter(p => p.IDEditorial == IDEditorial);
            }

            return Result;
        }



        // Devuelve una lista de Libros disponibles con stock, segun el nombre de la editorial.
        public List<Libro> FilterBookByEditorial(string NombreEditorial)
        {
            List<Libro> Resultado = null;

            using (var r = new Repository<Libro>())
            {

                Resultado = r.Filter(p => p.Editorial.Nombre.Contains(NombreEditorial))
                            .Select(p => new Libro
                            {
                                ISBN = p.ISBN,
                                IDEditorial = p.IDEditorial,
                                Titulo = p.Titulo,
                                Autor = p.Autor,
                                Paginas = p.Paginas,
                                Precio = p.Precio,
                                Stock = p.Stock,
                                Publicacion = p.Publicacion,
                                Descripcion = p.Descripcion,
                            })
                            .Where(p => p.Stock > 0)
                            .ToList();
            }

            return Resultado;
        }


        // Devuelve una lista del detalle de todos los libros y editoriales.
        public List<Detalle> GetAllRecord()
        {

            DetalleBL detallebl = new DetalleBL();
            return detallebl.getDetalle();

        }


        //public List<lib> GetAllEntities()
        //{
        //    List<T> Resultado = null;

        //    using (var r = new Repository<T>())
        //    {
        //        Resultado = r.GetAll();
        //    }

        //    return Resultado;
        //}


    }
}
