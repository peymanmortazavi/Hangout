using Hangout.Entities;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Hangout.DataAccess
{

	public interface IDataAccess
	{

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Add<T> (T entity) where T : IEntity;

        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        void Add<T> (IEnumerable<T> entities) where T : IEntity;

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        void Delete<T> (string id) where T : IEntity;

        /// <summary>
        /// Deletes the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        void Delete<T> (Expression<Func<T, bool>> query) where T : IEntity;

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> All<T> () where T : IEntity;

        /// <summary>
        /// Alls the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IQueryable<T> All<T> (Expression<Func<T,bool>> query) where T : IEntity;

		T Get<T> (string id) where T : IEntity;

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Update<T> (T entity) where T : IEntity;

        /// <summary>
        /// Updates the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="update">The update.</param>
        void Update<T> (Expression<Func<T, bool>> query, Action<T> update) where T : IEntity;

	}

}