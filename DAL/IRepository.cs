using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    // Se crea un interfaz generica para que pueda ser repositorio de cualquier entidad (Libros, Editorial).
    // Se implementa la interfaz IDisposable para que se destruya de manera correcta el contexto.
    // Se condiciona tipo TEntity para que sea una clase en donde a veces se asignara un valor null.

    public interface IRepository<T> : IDisposable where T : class
    {

        T Create(T toCreate);

        bool Delete(T toDelete);

        bool Update(T toUpdate);

        List<T> GetAll();

        List<T> GetAll(List<Expression<Func<T, object>>> includes);

        List<GetAllDataSP> GetAllRecord();

        // Se crea una expresion LINQ para que realize un filtro que requiera recuperar informacion de acuerdo a una condicion.
        T Retrieve(Expression<Func<T, bool>> criteria);

        // Permite recuperar una lista de entidades de acuerdo a un filtro y que se aplica a un conjunto de registros.
        List<T> Filter(Expression<Func<T,bool>> criteria);

        List<T> SelectQuery(Expression<Func<T, T>> criteria);

        List<T> SelectMany(Expression<Func<T, IEnumerable<T>>> criteria);


    }
}
