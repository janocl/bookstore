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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        BookStoreEntities Contexto = null;

        public Repository()
        {
            Contexto = new BookStoreEntities();
            Contexto.Configuration.ProxyCreationEnabled = false;
            Contexto.Configuration.AutoDetectChangesEnabled = false;
            Contexto.Configuration.ValidateOnSaveEnabled = false;
        }


        // Propiedad que devuelve un conjunto de Entidades
        private DbSet<TEntity> EntitySet
        {
            get
            {
                return Contexto.Set<TEntity>();
            }
        }


        public TEntity Create(TEntity toCreate)
        {
            TEntity Result = null;
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

        public bool Delete(TEntity toDelete)
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

        public bool Update(TEntity toUpdate)
        {
            bool Result = false;
            try
            {
                EntitySet.Attach(toUpdate);
                // Se modifica el estado del objeto para indicar a EF que ha sufrido una modificacion y que debe aplicarla en la DB.
                Contexto.Entry<TEntity>(toUpdate).State = EntityState.Modified; 
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


        public TEntity Retrieve(Expression<Func<TEntity, bool>> criteria)
        {
            TEntity Result = null;

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

        public List<TEntity> Filter(Expression<Func<TEntity, bool>> criteria)
        {
            List<TEntity> Result = null;
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



        public List<TEntity> SelectQuery(Expression<Func<TEntity, TEntity>> criteria)
        {
            List<TEntity> Result = null;
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

        public List<TEntity> SelectMany(Expression<Func<TEntity, IEnumerable<TEntity>>> criteria)
        {
            List<TEntity> Result = null;

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
