using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Hangout.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using CAILMobility.DataAccess;

namespace Hangout.DataAccess
{
	public class MongoDataAccess : DataAccessBase, IDataAccess
	{

		private readonly Dictionary<string, MongoCollection> _collections;

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private MongoCollection GetCollection<T>()
		{

			var type = typeof(T);

			MongoCollection mongoCollection;
			if (_collections.TryGetValue(type.FullName, out mongoCollection))
				return mongoCollection;

			var collectionName = TypeMap.IsTypeMapRegistered(type) ?
				TypeMap.GetTypeMap(type).CollectionName :
				type.Name;

			mongoCollection = Database.GetCollection(collectionName);

			_collections[type.FullName] = mongoCollection;

			return mongoCollection;

		}

        /// <summary>
        /// Registers the conventions.
        /// </summary>
        private static void RegisterConventions()
		{
			var myConventions = new ConventionPack
			{
				new NamedIdMemberConvention(new[] {"Id"}, MemberTypes.Property),
				new CamelCaseElementNameConvention(),
				new IgnoreExtraElementsConvention(true),
				new MemberSerializationOptionsConvention(typeof (DateTime),
					DateTimeSerializationOptions.UtcInstance)
			};

			ConventionRegistry.Register(
				"Default Camel Case",
				myConventions,
				t => true);
		}


        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MongoDataAccess(string connectionString)
			: base(connectionString)
		{
			_collections = new Dictionary<string, MongoCollection>();

			RegisterConventions();
		}

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Get<T>(string id) where T : IEntity
		{
			return GetCollection<T>().FindOneByIdAs<T>(id);
		}

        /// <summary>
        /// Gets the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public T Get<T>(Expression<Func<T, bool>> query) where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>().FirstOrDefault(query);
		}

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> All<T>() where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>();
		}

        /// <summary>
        /// Alls the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IQueryable<T> All<T>(Expression<Func<T, bool>> query) where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>().Where(query);
		}

        // TODO: find a way to use an id generator
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void Add<T>(T entity) where T : IEntity
		{
			if (string.IsNullOrWhiteSpace(entity.Id))
				entity.Id = ObjectId.GenerateNewId().ToString();

			GetCollection<T>().Save(entity);
		}

        // TODO: find a way to use an id generator
        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        public void Add<T>(IEnumerable<T> entities) where T : IEntity
		{
			foreach (var entity in entities)
			{
				if (string.IsNullOrWhiteSpace(entity.Id))
					entity.Id = ObjectId.GenerateNewId().ToString();
			}

			GetCollection<T>().InsertBatch(entities);
		}

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        public void Delete<T>(string id) where T : IEntity
		{
			GetCollection<T>().Remove(Query<T>.EQ(record => record.Id, id));
		}

        /// <summary>
        /// Deletes the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        public void Delete<T>(Expression<Func<T, bool>> query) where T : IEntity
		{

			var mongoCollection = GetCollection<T>();

			var mongoQuery = (MongoQueryable<T>)mongoCollection.AsQueryable<T>().Where(query);

			mongoCollection.Remove(mongoQuery.GetMongoQuery());

		}

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        public void Update<T>(T entity) where T : IEntity
		{
			GetCollection<T>().Save(entity);
		}

        /// <summary>
        /// Updates the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="update">The update.</param>
        public void Update<T>(Expression<Func<T, bool>> query, Action<T> update) where T : IEntity
		{
			var mongoCollection = GetCollection<T>();

			var mongoQuery = (MongoQueryable<T>)mongoCollection.AsQueryable<T>().Where(query);

			foreach (var item in mongoQuery.ToList())
			{
				update(item);
				mongoCollection.Save(item);
			}
		}
	}
}