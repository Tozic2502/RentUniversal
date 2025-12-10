using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentUniversal.Application.Interfaces;

public interface IMongoDbSettings
{
    string ConnectionString { get; set; }
    string Database { get; set; }
}