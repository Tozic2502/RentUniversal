using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentUniversal.Application.Interfaces;

namespace RentUniversal.Infrastructure.Configuration
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string Database { get; set; } = "RentUniversal";
    }
}