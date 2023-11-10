using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL;

// Rafal
public class EmailDao : BaseDao
{
    private readonly IMongoCollection<User> _userCollection;

    public EmailDao()
    {
        _userCollection = GetUserCollection();
    }

    public void AddPasswordResetToken(string userUsername, string token)
    {
        var filter = Builders<User>.Filter.Eq("username", userUsername);
        var update = Builders<User>.Update.Set("passwordResetToken", token);
        _userCollection.UpdateOne(filter, update);
    }

    public string? GetPasswordResetToken(string userUsername)
    {
        // get only passwordResetToken field from user document
        var projection = Builders<User>.Projection.Include("passwordResetToken").Exclude("_id");
        var userToken = _userCollection.Find(new BsonDocument("username", userUsername)).Project(projection)
            .FirstOrDefault();
        var token = "";

        if (userToken.Contains("passwordResetToken"))
        {
            token = userToken["passwordResetToken"].AsString;
        }
        else
        {
            return null;
        }

        return token;
    }

    public void ChangePassword(string userUsername, string newPasswordHashed)
    {
        var filter = Builders<User>.Filter.Eq("username", userUsername);
        var update = Builders<User>.Update.Set("password", newPasswordHashed);
        _userCollection.UpdateOne(filter, update);
    }
}