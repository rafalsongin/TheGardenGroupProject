using System.Collections.ObjectModel;
using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DAL;

public class UserDao
{

    private readonly BaseDao _baseDao;
    private IMongoCollection<User> _userCollection;

}