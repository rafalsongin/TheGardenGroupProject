﻿using DAL;
using Model;

namespace Service;

// Rafal
public class VerifyingLoginService
{
    private readonly UserDao _userDb;

    public VerifyingLoginService()
    {
        _userDb = new UserDao();
    }

    public bool IsCorrectPassword(string? inputUsername, string inputPassword)
    {
        if (_userDb?.GetUserByUsername(inputUsername) == null)
        {
            return false;
        }

        // get hashed password from the database
        User user = _userDb.GetUserByUsername(inputUsername);
        string hashedPassword = user.Password;

        // check if the password is correct
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);

        return passwordMatch;
    }

    public string HashPassword(string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashedPassword;
    }
}