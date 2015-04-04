using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BLL;
using Entities;

namespace SL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class BookStore : IBookStore
    {

        private LibrosBL libros = null;
        private EditorialesBL editoriales = null;

        private BookStore()
        {
            libros = new LibrosBL();
            editoriales = new EditorialesBL();       
        }


        public Libro CreateBook(Libro newBook)
        {            
            try
            {
                newBook = libros.Create(newBook);
            }
            catch (Exception ex)
            {                
                throw(new FaultException(ex.Message, new FaultCode("NombreDuplicado")));
            }

            return newBook;
        }



        public Libro RetrieveBookByID(short ID)
        {
            var Result = libros.RetrieveById(ID);
            return Result;
        }



        public bool UpdateBook(Libro bookToUpdate)
        {
            bool Result = false;
        
            try
            {                
                Result = libros.Update(bookToUpdate);
            }
            catch (Exception ex)
            {

                throw (new FaultException(ex.Message, new FaultCode("NombreDuplicado")));
            }

            return Result;
        }




        public bool DeleteBook(short ID)
        {
            bool Result = false;
            try
            {
                Result = libros.Delete(ID);
            }
            catch (Exception ex)
            {

                throw (new FaultException(ex.Message, new FaultCode("ConExitencias")));
            }

            return Result;
        }


        public List<Libro> GetAllBooks()
        {
            List<Libro> Result = libros.GetAllBooks();
            return Result;
        }


        public List<Editorial> GetAllEditoriales()
        {
            List<Editorial> Result = editoriales.GetAllEditoriales();
            return Result;        
        }

        public List<Detalle> GetAllRecord()
        {
            List<Detalle> Result = libros.GetAllRecord();
            return Result;
        }


        public List<Libro> FilterBookByEditorialID(short IDCategory)
        {
            List<Libro> Result = libros.FilterByEditorialID(IDCategory);
            return Result;
        }


        public List<Libro> FilterBookByTitulo(string titulo)
        {
            List<Libro> Result = libros.FilterBookByTitulo(titulo);
            return Result;        
        }


        public Editorial CreateEditorial(Editorial newEditorial)
        {
            var Result = editoriales.Create(newEditorial);
            return Result;
        }


        public List<Libro> FilterBookByEditorial(string nombreEditorial)
        {
            var Result = libros.FilterBookByEditorial(nombreEditorial);
            return Result;        
        }




    }
}
