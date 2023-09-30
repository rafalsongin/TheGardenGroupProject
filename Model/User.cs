using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model;

public class User
{
    public User(ObjectId objectId , string username, string password, string firstName, string lastName, UserType userType, string email, string phoneNumber, City city)
    {
        ObjectId = objectId;
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        Email = email;
        PhoneNumber = phoneNumber;
        City = city;
        PasswordResetToken = null;
    }

    [BsonElement("_id")]
    public ObjectId ObjectId { get; set; }
    
    [BsonElement("username")]
    public string Username { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("firstName")]
    public string FirstName { get; set; }
    
    [BsonElement("lastName")]
    public string LastName { get; set; }
    
    [BsonElement("userType")]
    public UserType UserType { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; }
    
    [BsonElement("city")]
    public City City { get; set; }
    
    [BsonElement("passwordResetToken")]
    public string? PasswordResetToken { get; set; }
}