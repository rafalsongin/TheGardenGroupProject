using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model;

public class User
{
    //dana
    public User(string username, string password, string firstName, string lastName, UserType userType, string email,
        string phoneNumber, City city)
    {
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

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("username")] public string Username { get; set; }

    [BsonElement("password")] public string Password { get; set; }

    [BsonElement("firstName")] public string FirstName { get; set; }

    [BsonElement("lastName")] public string LastName { get; set; }

    [BsonElement("userType")]
    [BsonRepresentation(BsonType.String)]
    public UserType UserType { get; set; }

    [BsonElement("email")] public string Email { get; set; }

    [BsonElement("phoneNumber")] public string PhoneNumber { get; set; }

    [BsonElement("city")]
    [BsonRepresentation(BsonType.String)]
    public City City { get; set; }

    [BsonElement("passwordResetToken")] public string? PasswordResetToken { get; set; }

    [BsonElement("amountOfTickets")] public string AmountOfTickets { get; set; }

    public override string ToString()
    {
        return $"User: {FirstName} {LastName} {Email}";
    }
}