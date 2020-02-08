using Cafe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafe.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private dbCafeEntities DBEntity = new dbCafeEntities();
        public IRepository<TEntityType> GetRepositoryInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepository<TEntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}