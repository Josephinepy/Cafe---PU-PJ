using Cafe.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cafe.Repository
{
    public class GenericRepository<Entity> : IRepository<Entity> where Entity : class
    {
        DbSet<Entity> _dbSet;

        private dbCafeEntities _DBEntity;

        public GenericRepository(dbCafeEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<Entity>();
        }
        public IEnumerable<Entity> GetProduct()
        {
            return _dbSet.ToList();
        }
        public IEnumerable<Entity> GetCart()
        {
            return _dbSet.ToList();
        }
        public IEnumerable<Entity> GetOrder()
        {
            return _dbSet.ToList();
        }

        public void Add(Entity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public int GetAllrecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public Entity GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public Entity GetFirstorDefaultByParameter(Expression<Func<Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Entity> GetListParameter(Expression<Func<Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _DBEntity.Database.SqlQuery<Entity>(query, parameters).ToList();
            }
            else
                return _DBEntity.Database.SqlQuery<Entity>(query).ToList();
        }

        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(Entity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void RemovebyWhereClause(Expression<Func<Entity, bool>> wherePredict)
        {
            Entity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeBywhereClause(Expression<Func<Entity, bool>> wherePredict)
        {
            List<Entity> entity = _dbSet.Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        public void Update(Entity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }


        public void UpdateByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public IEnumerable<Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Entity, bool>> wherePredict, Expression<Func<Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();

            }

            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }
    }
}