using Model;
using MongoDB.Driver;

namespace DAL;

// Rafal
public class BaseDao
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    protected BaseDao()
    {
        _client = new MongoClient("mongodb+srv://dbUser:9BPqGfB5pEvENADf@thegardengroupserver.cbkve.mongodb.net/");
        _database = _client.GetDatabase("Database");
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

    protected IMongoCollection<Ticket> GetArchivedTicketCollection()
    {
        var database = _client.GetDatabase("Database_Archive");
        return database.GetCollection<Ticket>("ClosedTickets_Archive");
    }
}