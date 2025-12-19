using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentUniversal.Application.Interfaces;


namespace RentUniversal.Infrastructure.Configuration
{
    /// <summary>
    /// Represents the configuration settings required to connect to a MongoDB database.
    /// This class provides the connection string and database name, making it easier to
    /// configure and access MongoDB in a universal and centralized manner.
    /// </summary>
    public class MongoDbSettings : IMongoDbSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to connect to the MongoDB server.
        /// Default value points to a local MongoDB instance at "mongodb://localhost:27017".
        /// </summary>
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        /// <summary>
        /// Gets or sets the name of the MongoDB database to be used.
        /// Default value is "RentUniversal".
        /// </summary>
        public string Database { get; set; } = "RentUniversal";
    }
}