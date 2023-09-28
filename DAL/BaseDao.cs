using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

namespace DAL
{
    public class BaseDao
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public BaseDao()
        {
            _client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
            _database = _client.GetDatabase("TheGardenGroup");
        }

        public List<DatabasesModel> GetDatabases()
        {
            List<DatabasesModel> allDatabases = new List<DatabasesModel>();

            foreach (BsonDocument db in _client.ListDatabases().ToList())
            {
                allDatabases.Add(BsonSerializer.Deserialize<DatabasesModel>(db));
            }
            return allDatabases;
        }
        
        // Add your GetCollection methods here below
        public IMongoCollection<User> GetUserCollection()
        {
            return _database.GetCollection<User>("User");
        }
    }
}