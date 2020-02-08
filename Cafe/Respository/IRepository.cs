using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cafe.Repository
{
    public interface IRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetProduct();
        IEnumerable<Entity> GetCart();
        IEnumerable<Entity> GetOrder();
        IEnumerable<Entity> GetAllRecords();
        IQueryable<Entity> GetAllRecordsIQueryable();
        int GetAllrecordCount();
        void Add(Entity entity);
        void Update(Entity entity);
        void UpdateByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict);
        Entity GetFirstorDefault(int recordId);
        void Remove(Entity entity);
        void RemovebyWhereClause(Expression<Func<Entity, bool>> wherePredict);
        void RemoveRangeBywhereClause(Expression<Func<Entity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<Entity, bool>> wherePredict, Action<Entity> ForEachPredict);
        Entity GetFirstorDefaultByParameter(Expression<Func<Entity, bool>> wherePredict);
        IEnumerable<Entity> GetListParameter(Expression<Func<Entity, bool>> wherePredict);
        IEnumerable<Entity> GetResultBySqlprocedure(string query, params object[] parameters);
        IEnumerable<Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Entity, bool>> wherePredict, Expression<Func<Entity, int>> orderByPredict);

    }
}