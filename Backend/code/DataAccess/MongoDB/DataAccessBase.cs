using MongoDB.Driver;


namespace Hangout.DataAccess
{
	public abstract class DataAccessBase
	{
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        protected MongoDatabase Database { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessBase"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected DataAccessBase(string connectionString)
		{
			var url = MongoUrl.Create(connectionString);

			var mongoClient = new MongoClient(url);
			var mongoServer = mongoClient.GetServer();

			Database = mongoServer.GetDatabase(url.DatabaseName);
		}

        /// <summary>
        /// Maps the objects.
        /// </summary>
        protected virtual void MapObjects()
		{   
		}
	}
}