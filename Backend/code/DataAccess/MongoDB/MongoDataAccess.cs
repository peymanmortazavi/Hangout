using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Hangout.Entities;
using System.Linq.Expressions;
using System.Linq;
using MongoDB.Driver.Linq;
using CAILMobility.DataAccess;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Hangout.DataAccess
{
	public class MongoDataAccess : DataAccessBase, IDataAccess
	{

		private readonly Dictionary<string, MongoCollection> _collections;

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


		public MongoDataAccess(string connectionString)
			: base(connectionString)
		{
			_collections = new Dictionary<string, MongoCollection>();

			RegisterConventions();
		}

		public T Get<T>(string id) where T : IEntity
		{
			return GetCollection<T>().FindOneByIdAs<T>(id);
		}

		public T Get<T>(Expression<Func<T, bool>> query) where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>().FirstOrDefault(query);
		}

		public IQueryable<T> All<T>() where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>();
		}

		public IQueryable<T> All<T>(Expression<Func<T, bool>> query) where T : IEntity
		{
			return GetCollection<T>().AsQueryable<T>().Where(query);
		}

		// TODO: find a way to use an id generator
		public void Add<T>(T entity) where T : IEntity
		{
			if (string.IsNullOrWhiteSpace(entity.Id))
				entity.Id = ObjectId.GenerateNewId().ToString();

			GetCollection<T>().Save(entity);
		}

		// TODO: find a way to use an id generator
		public void Add<T>(IEnumerable<T> entities) where T : IEntity
		{
			foreach (var entity in entities)
			{
				if (string.IsNullOrWhiteSpace(entity.Id))
					entity.Id = ObjectId.GenerateNewId().ToString();
			}

			GetCollection<T>().InsertBatch(entities);
		}

		public void Delete<T>(string id) where T : IEntity
		{
			GetCollection<T>().Remove(Query<T>.EQ(record => record.Id, id));
		}

		public void Delete<T>(Expression<Func<T, bool>> query) where T : IEntity
		{

			var mongoCollection = GetCollection<T>();

			var mongoQuery = (MongoQueryable<T>)mongoCollection.AsQueryable<T>().Where(query);

			mongoCollection.Remove(mongoQuery.GetMongoQuery());

		}

		public void Update<T>(T entity) where T : IEntity
		{
			GetCollection<T>().Save(entity);
		}

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