using Model;
using MongoDB.Driver;

namespace DAL;

public class UserDao : BaseDao
{
    private readonly IMongoCollection<User> _userCollection;

    public UserDao()
    {
        _userCollection = GetUserCollection();
    }

    public User GetUserByUsername(string? username)
    {
        var filter = Builders<User>.Filter.Eq("username", username);
        var user = _userCollection.Find(filter).FirstOrDefault();

        return user;
    }

    public List<User> GetAllUsers()
    {
        var filter = Builders<User>.Filter.Empty;

        // Define the projection to exclude the "_id" field
        var projection = Builders<User>.Projection.Exclude(u => u.Id);

        var userList = _userCollection.Find(filter).Project<User>(projection).ToList();

        return userList;
    }


    public void AddUser(User user)
    {
        _userCollection.InsertOne(user);
    }
}