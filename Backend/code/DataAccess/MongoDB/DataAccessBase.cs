using MongoDB.Driver;


namespace Hangout.DataAccess
{
	public abstract class DataAccessBase
	{
		protected MongoDatabase Database { get; private set; }

		protected DataAccessBase(string connectionString)
		{
			var url = MongoUrl.Create(connectionString);

			var mongoClient = new MongoClient(url);
			var mongoServer = mongoClient.GetServer();

			Database = mongoServer.GetDatabase(url.DatabaseName);
		}

		protected virtual void MapObjects()
		{   
		}
	}
}