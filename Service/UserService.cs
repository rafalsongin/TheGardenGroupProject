using DAL;
using Model;

namespace Service;

public class UserService
{
    private readonly UserDao _userDao;

    public UserService(UserDao userDao)
    {
        _userDao = userDao;
    }
    
    public List<User> GetAllUsers()
    {
        return _userDao.GetAllUsers();
    }
}