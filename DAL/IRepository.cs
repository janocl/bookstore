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

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {

        TEntity Create(TEntity toCreate);

        bool Delete(TEntity toDelete);

        bool Update(TEntity toUpdate);

        List<GetAllDataSP> GetAllRecord();

        // Se crea una expresion LINQ para que realize un filtro que requiera recuperar informacion de acuerdo a una condicion.
        TEntity Retrieve(Expression<Func<TEntity, bool>> criteria);

        // Permite recuperar una lista de entidades de acuerdo a un filtro y que se aplica a un conjunto de registros.
        List<TEntity> Filter(Expression<Func<TEntity,bool>> criteria);

        List<TEntity> SelectQuery(Expression<Func<TEntity, TEntity>> criteria);

        List<TEntity> SelectMany(Expression<Func<TEntity, IEnumerable<TEntity>>> criteria);


    }
}
