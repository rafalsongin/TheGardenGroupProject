using System.Collections.ObjectModel;
using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DAL;

public class UserDao
{
    private readonly IMongoCollection<User> _userCollection;

    public UserDao()
    {
        var baseDao = new BaseDao();
        _userCollection = baseDao.GetUserCollection();
    }

    public User GetUserByUsername(string username)
    {
        var filter = Builders<User>.Filter.Eq("username", username);
        var user = _userCollection.Find(filter).FirstOrDefault();
        
        return user;
    }
    
    public List<User> GetAllUsers()
    {
        var filter = Builders<User>.Filter.Empty;
        var userList = _userCollection.Find(filter).ToList();
        
        return userList;
    }
}