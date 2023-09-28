﻿using MongoDB.Driver;

namespace DAL;

public class UserDao
{
    private readonly MongoClient _client;
    private readonly BaseDao _baseDao;
    private IMongoCollection<Ticket> _ticketCollection;
    private IMongoDatabase _database;
    
    public UserDao()
    {
        _baseDao = new BaseDao();
        _client = _baseDao.GetMongoClient();
        _database = _client.GetDatabase("TheGardenGroup");//geting the database
        _ticketCollection = _database.GetCollection<Ticket>("IncidentTicket");//the table that we want to work on
    }
    
    public void GetUsers()
    {
        
    }
}