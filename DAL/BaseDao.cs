using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

namespace DAL
{
    public class Dao
    {
        private MongoClient client;

        public Dao()
        {
            client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
        }

        public List<Databases_Model> GetDatabases()
        {
            List<Databases_Model> allDatabases = new List<Databases_Model>();

            foreach (BsonDocument db in client.ListDatabases().ToList())
            {
                allDatabases.Add(BsonSerializer.Deserialize<Databases_Model>(db));
            }
            return allDatabases;
        }
    }
}