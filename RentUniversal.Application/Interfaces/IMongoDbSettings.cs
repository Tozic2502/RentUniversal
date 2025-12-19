namespace RentUniversal.Application.Interfaces
{
    /// <summary>
    /// Abstraction for MongoDB configuration settings.
    /// and injected where database connectivity needs to be configured.
    /// </summary>
    public interface IMongoDbSettings
    {
        /// <summary>
        /// MongoDB connection string used to connect to the server.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Name of the MongoDB database to use.
        /// </summary>
        string Database { get; set; }
    }
}