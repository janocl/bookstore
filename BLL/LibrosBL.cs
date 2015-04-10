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
        private readonly IRepository<Libro> irepo;

        public LibrosBL()
            : this(new Repository<Libro>())
        { }

        public LibrosBL(IRepository<Libro> repository) 
        {
            irepo = repository;
        }

        // Crea un nuevo libro.
        public Libro Create(Libro newLibro)
        {
            Libro Result = null;

            try
            {
                Libro libBuscado = irepo.Retrieve(p => p.Titulo == newLibro.Titulo);

                // Si no existe entonces lo creamos.
                if (libBuscado == null)
                {
                    Result = irepo.Create(newLibro);
                }
                else
                {
                    throw (new Exception("El Titulo de Libro especificado ya existe..."));
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return Result;
        }



        // Actualiza los registros de un libro.
        public bool Update(Libro libroToUpdate)
        {
            bool Result = false;

            try
            {
                Libro libBuscado = irepo.Retrieve(p => p.Titulo == libroToUpdate.Titulo);

                // Si no existe entonces lo creamos.
                if (libBuscado == null)
                {
                    Result = irepo.Update(libroToUpdate);
                }
                else
                {
                    throw (new Exception("El Titulo de Libro especificado ya existe"));
                }
            }
            catch (Exception)
            {
                
                throw;
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
                    try
                    {
                        Result = irepo.Delete(Libro);
                    }
                    catch (Exception)
                    {
                        
                        throw;
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
            try
            {
                Result = irepo.SelectQuery(p => new Libro()
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
            catch (Exception)
            {
                throw;
            }

            return Result;
        }



        // Devuelve un lista de libros por su ID
        public Libro RetrieveById(short ID)
        {
            Libro Result = null;

            try
            {
                Result = irepo.Retrieve(p => p.IDLibro == ID);
            }
            catch (Exception)
            {
                throw;
            }

            return Result;
        }



        // Devuelve una lista de libros de acuerdo al titulo del libro.
        public List<Libro> FilterBookByTitulo(string titulo)
        {
            List<Libro> Result = null;
            try
            {
                Result = irepo.Filter(p => p.Titulo.Contains(titulo));
            }
            catch (Exception)
            {
                throw;
            }

            return Result;
        }



        // Devuelve una lista de Libros por el ID de la Editorial, siempre que el libro exista.
        public List<Libro> FilterByEditorialID(short IDEditorial)
        {
            List<Libro> Result = null;
            try
            {
                var query = irepo.Filter(p => p.IDEditorial == IDEditorial);
                //Seleccionamos solo las propiedades que necesitemos.
                Result = query.Select(x => new Libro()
                                        {
                                            IDLibro = x.IDLibro,
                                            Titulo = x.Titulo,
                                            ISBN = x.ISBN,
                                            Autor = x.Autor,
                                            Descripcion = x.Descripcion,
                                            Stock = x.Stock,
                                            Publicacion = x.Publicacion,
                                            Precio = x.Precio,
                                            Paginas = x.Paginas
                                        }).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return Result;
        }



        // Devuelve una lista de Libros disponibles con stock, segun el nombre de la editorial.
        public List<Libro> FilterBookByEditorial(string NombreEditorial)
        {
            List<Libro> Resultado = null;

            try
            {
                Resultado = irepo.Filter(p => p.Editorial.Nombre.Contains(NombreEditorial))
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
            catch (Exception)
            {
                throw;
            }

            return Resultado;
        }


        // Devuelve una lista del detalle de todos los libros y editoriales.
        public List<Detalle> GetAllRecord()
        {
            try
            {
                DetalleBL detallebl = new DetalleBL();
                return detallebl.getDetalle();
            }
            catch (Exception)
            {
                throw;
            }

        }



    }
}
