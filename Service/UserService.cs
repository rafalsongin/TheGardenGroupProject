﻿using DAL;
using Model;

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
}