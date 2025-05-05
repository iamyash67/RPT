using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;

namespace RPT.Services;

public class LoginService: ILoginService
{
    private readonly DbService _dbs;

    public LoginService(DbService dbs)
    {
        _dbs = dbs;
    }

public Profile? Login(string username, string password)
{
    using var conn = _dbs.GetConnection();
    conn.Open();

    var cmd = new MySqlCommand("CALL ValidateLogin(@UserName, @Password)", conn);
    cmd.Parameters.AddWithValue("@UserName", username);
    cmd.Parameters.AddWithValue("@Password", password);

    using var reader = cmd.ExecuteReader();
    return reader.Read() ? new Profile
    {
        ProfileId = reader.GetInt32("ProfileId"),
        FirstName = reader.GetString("FirstName"),
        LastName = reader.GetString("LastName"),
        Age = reader.GetInt32("Age"),
        Gender = reader.GetString("Gender"),
        UserName = reader.GetString("UserName"),
        Password = reader.GetString("Password")
    } : null;
}

}
