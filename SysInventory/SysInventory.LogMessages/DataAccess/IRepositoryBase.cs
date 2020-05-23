﻿using System.Collections.Generic;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess
{
    internal interface IRepositoryBase<T> where T : IIdentifiable
    {
        /// <summary>
        /// Returns a single model object of type M, which is loaded using the transferred PrimaryKey.
        /// </summary>
        /// <typeparam name="TKey">PrimaryKey type</typeparam>
        /// <param name="pkValue">PrimaryKey value</param>
        /// <returns>found Model-Object or null</returns>
        T GetSingle<TKey>(TKey pkValue);

        /// <summary>
        /// Adds a model object to the database
        /// </summary>
        /// <param name="entity">object to sotre</param>
        void Add(T entity);

        /// <summary>
        /// Deletes a model object from database
        /// </summary>
        /// <param name="entity">object to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Updates an object
        /// </summary>
        /// <param name="entity">object to update</param>
        void Update(T entity);

        /// <summary>
        /// Returns a list of model objects of type M that were loaded according to the Where condition. The values of the Where condition can be passed separately so that they can be used for PreparedStatements. (Prevention of SQL injection)
        /// </summary>
        /// <param name="whereCondition">Wherecondition as string</param>
        /// <param name="parameterValues">Parameter-Values</param>
        /// <returns></returns>
        List<T> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        /// <summary>
        /// Returns all object of this type
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Counts all the objects which matches the where criteria
        /// </summary>
        /// <param name="whereCondition">where criteria as string</param>
        /// <param name="parameterValues">Parameter-Values</param>
        /// <returns></returns>
        long Count(string whereCondition, Dictionary<string, object> parameterValues);

        /// <summary>
        /// Counts object of given type
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// Returns the table name
        /// </summary>
        string TableName { get; }
    }
}
