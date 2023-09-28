using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

namespace DAL
{
    public class BaseDao
    {
        private MongoClient client;

        public BaseDao()
        {
            client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
        }

        public List<DatabasesModel> GetDatabases()
        {
            List<DatabasesModel> allDatabases = new List<DatabasesModel>();

            foreach (BsonDocument db in client.ListDatabases().ToList())
            {
                allDatabases.Add(BsonSerializer.Deserialize<DatabasesModel>(db));
            }
            return allDatabases;
        }
    }
}