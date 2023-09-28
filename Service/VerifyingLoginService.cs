namespace Service;

using BCrypt.Net;

public class VerifyingLoginService
{
    /*// TODO: hash passwords in the database
    private EmployeeDao employeeDb;

    public VerifyingService()
    {
        employeeDb = new EmployeeDao();
    }

    public bool isCorrectPassword(string inputUsername, string inputPassword)
    {
        if (employeeDb.GetEmployeeByUsername(inputUsername) == null)
        {
            return false;
        }

        // get hashed password from the database
        Employee employee = employeeDb.GetEmployeeByUsername(inputUsername);
        string hashedPassword = employee.LoginPassword;

        // check if the password is correct
        bool passwordMatch = BCrypt.Verify(inputPassword, hashedPassword);

        return passwordMatch;
    }*/
}