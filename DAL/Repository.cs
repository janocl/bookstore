using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        BookStoreEntities Contexto = null;

        public Repository()
        {
            Contexto = new BookStoreEntities();
        }


        // Propiedad que devuelve un conjunto de Entidades
        private DbSet<T> EntitySet
        {
            get
            {
                return Contexto.Set<T>();
            }
        }


        public T Create(T toCreate)
        {
            T Result = null;
            try
            {
                // Se agrega una entidad a un conjunto de entidades.
                EntitySet.Add(toCreate);
                Contexto.SaveChanges();
                Result = toCreate;
            }
            catch (Exception)
            {              
                throw;
            }

            return Result;
        }

        public bool Delete(T toDelete)
        {

            bool Result = false;
            try
            {
                // Se atacha la entidad al modelo.
                EntitySet.Attach(toDelete);
                EntitySet.Remove(toDelete);
                // Si es mayor a cero se convierte en True y se elimina.
                Result = Contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {               
                throw;
            }

            return Result;
        }

        public bool Update(T toUpdate)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toUpdate);
                // Se modifica el estado del objeto para indicar a EF que ha sufrido una modificacion y que debe aplicarla en la DB.
                Contexto.Entry<T>(toUpdate).State = EntityState.Modified; 
                Result = Contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {               
                throw;
            }

            return Result;
        }

        public List<GetAllDataSP> GetAllRecord() 
        {
            List<GetAllDataSP> Result = null;

            try
            {
                Result = Contexto.GetAllData().ToList();
            }
            catch (Exception)
            {
                
                throw;
            }

            return Result;
        
        }


        public T Retrieve(Expression<Func<T, bool>> criteria)
        {
            T Result = null;

            try
            {
                Result = EntitySet.FirstOrDefault(criteria);            
            }
            catch (Exception)
            {                
                throw;
            }

            return Result;
        }

        public List<T> Filter(Expression<Func<T, bool>> criteria)
        {
            List<T> Result = null;
            try
            {
                Result = EntitySet.Where(criteria).ToList();
            }
            catch (Exception)
            {
                
                throw;
            }

            return Result;
        }



        public List<T> SelectQuery(Expression<Func<T, T>> criteria)
        {
            List<T> Result = null;
            try
            {
                Result = EntitySet.Select(criteria.Compile()).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return Result;
        }

        public List<T> SelectMany(Expression<Func<T, IEnumerable<T>>> criteria)
        {
            List<T> Result = null;

            try
            {
                Result = EntitySet.SelectMany(criteria.Compile()).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return Result;
        }



        public void Dispose()
        {
            if (Contexto != null)
            {
                Contexto.Dispose();  
            }
            GC.SuppressFinalize(this);
        }
    }
}
