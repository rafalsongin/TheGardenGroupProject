using DAL;

namespace Service;

public class UserService
{
    private UserDao _userDao;

    public UserService(UserDao userDao)
    {
        _userDao = userDao;
    }
}