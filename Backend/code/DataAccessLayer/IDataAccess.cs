using Hangout.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace Hangout.DataAccess
{

	public interface IDataAccess
	{

		void Add<T> (T entity) where T : IEntity;

		void Remove<T> (string id) where T : IEntity;

		void Delete<T> (string id) where T : IEntity;

		IQueryable<T> All<T> () where T : IEntity;

		IQueryable<T> Get<T> (Expression<Func<T, bool>> query) where T : IEntity;

		T Get<T> (string id) where T : IEntity;

	}

}