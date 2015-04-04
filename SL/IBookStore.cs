using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entities;

namespace SL
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IBookStore
    {

        [OperationContract]
        Libro CreateBook(Libro newBook);

        [OperationContract]
        Libro RetrieveBookByID(short ID);

        [OperationContract]
        bool UpdateBook(Libro bookToUpdate);

        [OperationContract]
        bool DeleteBook(short ID);

        [OperationContract]
        List<Libro> GetAllBooks();

        [OperationContract]
        List<Editorial> GetAllEditoriales();

        [OperationContract]
        List<Detalle> GetAllRecord();

        [OperationContract]
        List<Libro> FilterBookByEditorialID(short IDCategory);

        [OperationContract]
        List<Libro> FilterBookByTitulo(string titulo);

        [OperationContract]
        Editorial CreateEditorial(Editorial newEditorial);

        [OperationContract]
        List<Libro> FilterBookByEditorial(string nombreEditorial);

        





    }


 
}
