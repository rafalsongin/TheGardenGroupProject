using DAL;
using Model;
using System.Net.Mail;

namespace Service;

//dana
public class UserService
{
    private readonly UserDao _userDao;
    private readonly TicketDao _ticketDao;

    public UserService()
    {
        _userDao = new UserDao();
        _ticketDao = new TicketDao();
    }
    
    public List<User> GetAllUsers()
    {
        return _userDao.GetAllUsers();
    }

    public User GetUserByUsername(string? username)
    {
        return _userDao.GetUserByUsername(username);
    }

    public bool IsUserCreatedAndAddedSuccessfully(string firstName, string lastName, string emailAddress, string phoneNumber, string city, string userType)
    {
        City newCity = GetCityEnum(city);
        UserType newUserType = GetUserTypeEnum(userType);

        // TODO: check if this is needed in the requirements
        //bool sendPassword = SendPassword_CheckBox.IsChecked ?? false;

        string username = firstName + lastName;
        string password = firstName + lastName + "login"; // hardcoded password
        User newUser = new User(username, password, firstName, lastName, newUserType, emailAddress, phoneNumber, newCity);

        Console.WriteLine(newUser.ToString());

        try
        {
            _userDao.AddUser(newUser);

            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
    }
    public long GetTicketCountForUser(string userEmail)
    {
        return _ticketDao.GetTicketCountForUser(userEmail);
    }


    private static UserType GetUserTypeEnum(string userType)
    {
        // declaring variable
        UserType newUserType = UserType.ServiceDeskEmployee; //a default value

        switch (userType)
        {
            case "Service desk employee":
                newUserType = UserType.ServiceDeskEmployee;
                break;
            case "Company employee":
                newUserType = UserType.CompanyEmployee;
                break;
            case "Manager":
                newUserType = UserType.Manager;
                break;
        }

        return newUserType;
    }

    private static City GetCityEnum(string city)
    {
        // declaring variable

        City newCity = City.Haarlem;

        switch (city)
        {
            case "Haarlem":
                newCity = City.Haarlem;
                break;
            case "Amsterdam":
                newCity = City.Amsterdam;
                break;
            case "DenHaag":
                newCity = City.DenHaag;
                break;
            case "Rotterdam":
                newCity = City.Rotterdam;
                break;
            case "Eindhoven":
                newCity = City.Eindhoven;
                break;
        }

        return newCity;
    }
}