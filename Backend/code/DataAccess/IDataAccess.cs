using Hangout.Entities;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Hangout.DataAccess
{

	public interface IDataAccess
	{

		void Add<T> (T entity) where T : IEntity;

		void Add<T> (IEnumerable<T> entities) where T : IEntity;

		void Delete<T> (string id) where T : IEntity;

		void Delete<T> (Expression<Func<T, bool>> query) where T : IEntity;

		IQueryable<T> All<T> () where T : IEntity;

		IQueryable<T> All<T> (Expression<Func<T,bool>> query) where T : IEntity;

		T Get<T> (string id) where T : IEntity;

		void Update<T> (T entity) where T : IEntity;

		void Update<T> (Expression<Func<T, bool>> query, Action<T> update) where T : IEntity;

	}

}