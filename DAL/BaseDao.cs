using System.Runtime.InteropServices.JavaScript;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

namespace DAL;

public class BaseDao
{
    protected MongoClient _client;
    protected IMongoDatabase _database;//protected for accessing it in the secondery database

    protected BaseDao()
    {
        // Server (testing)
        _client = new MongoClient("mongodb+srv://dbUser:9BPqGfB5pEvENADf@thegardengroupserver.cbkve.mongodb.net/");
            
        // Serverless
        // _client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
        _database = _client.GetDatabase("Database");
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
    protected IMongoCollection<User> GetUserCollection()
    {
        return _database.GetCollection<User>("User");
    }

    protected IMongoCollection<Ticket> GetTicketCollection()
    {
        return _database.GetCollection<Ticket>("Ticket");
    }
}