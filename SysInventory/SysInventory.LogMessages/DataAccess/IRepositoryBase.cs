using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SysInventory.LogMessages.DataAccess
{
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Adds a model object to the database
        /// </summary>
        /// <param name="entity">object to sotre</param>
        void Add(T entity);
        /// <summary>
        /// Counts object of given type
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// Counts all the objects which matches the where criteria
        /// </summary>
        /// <param name="whereCondition">where criteria as string</param>
        /// <param name="parameterValues">Parameter-Values</param>
        /// <returns></returns>
        long Count(string whereCondition, Dictionary<string, object> parameterValues);
        /// <summary>
        /// Counts all the objects which matches the where criteria
        /// </summary>
        /// <param name="whereExpression">where criteria as linq expression</param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// Deletes a model object from database
        /// </summary>
        /// <param name="entity">object to delete</param>
        void Delete(T entity);
        /// <summary>
        /// Returns all object of this type
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Returns a list of model objects of type M that were loaded according to the Where condition. The values of the Where condition can be passed separately so that they can be used for PreparedStatements. (Prevention of SQL injection)
        /// </summary>
        /// <param name="whereCondition">Wherecondition as string</param>
        /// <param name="parameterValues">Parameter-Values</param>
        /// <returns></returns>
        IQueryable<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues);
        /// <summary>
        /// Returns an IQueryble of model objects of type T that were loaded according to the Where condition.
        /// </summary>
        /// <param name="whereExpression">Wherecondition as linq expression</param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> whereExpression);
        /// <summary>
        /// Returns a single model object of type T, which is loaded using the transferred PrimaryKey.
        /// </summary>
        /// <typeparam name="TKey">PrimaryKey type</typeparam>
        /// <param name="pkValue">PrimaryKey value</param>
        /// <returns>found Model-Object or null</returns>
        T GetSingle<TKey>(TKey pkValue);
        /// <summary>
        /// Updates an object
        /// </summary>
        /// <param name="entity">object to update</param>
        void Update(T entity);
        void CleanUp();
    }
}
