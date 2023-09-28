﻿using System.Collections.ObjectModel;
using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DAL;

public class UserDao
{
    private readonly BaseDao _baseDao;
    private IMongoCollection<User> _userCollection;
    
    public UserDao(BaseDao baseDao)
    {
        _baseDao = new BaseDao();
        _userCollection = _baseDao.GetUserCollection();
    }
    
    public List<User> GetUsers()
    {
        
        
        return new List<User>();
    }
}