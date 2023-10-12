using DAL;
using Model;
using System.Net.Mail;

namespace Service;

public class UserService
{
    private readonly UserDao _userDao;

    public UserService()
    {
        _userDao = new UserDao();
    }
    
    public List<User> GetAllUsers()
    {
        return _userDao.GetAllUsers();
    }

    public User GetUserByUsername(string? username)
    {
        return _userDao.GetUserByUsername(username);
    }

    public void CreateAndAddUser(string firstName, string lastName, string emailAddress, string phoneNumber, string city, string userType)
    {
        City newCity = GetCityEnum(city);
        UserType newUserType = GetUserTypeEnum(userType);

        // TODO: check if this is needed in the requirements
        //bool sendPassword = SendPassword_CheckBox.IsChecked ?? false;

        string username = firstName + lastName;
        string password = firstName + lastName + "login"; // hardcoded password
        User newUser = new User(username, password, firstName, lastName, newUserType, emailAddress, phoneNumber, newCity);

        _userDao.AddUser(newUser);
    }

    private static UserType GetUserTypeEnum(string userType)
    {
        // declaring variable, otherwise doesn't work
        UserType newUserType = UserType.ServiceDeskEmployee;
        if (userType == "Service desk employee")
        {
            newUserType = UserType.ServiceDeskEmployee;
        }

        return newUserType;
    }

    private static City GetCityEnum(string city)
    {
        // declaring variable, otherwise doesn't work
        City newCity = City.Haarlem;
        if (city == "Haarlem")
        {
            newCity = City.Haarlem;
        }

        return newCity;
    }
}