using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;

namespace RPT.Services;

public class SignupService:ISignupService
{
    private readonly DbService _dbs;

    public SignupService(DbService dbs)
    {
        _dbs = dbs;
    }

    public bool Signup(Profile profile)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Profiles WHERE UserName = @UserName", conn);
        checkCmd.Parameters.AddWithValue("@UserName", profile.UserName);
        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
            return false;

        var cmd = new MySqlCommand("CALL CreateProfile(@FirstName, @LastName, @Age, @Gender, @UserName, @Password)", conn);
        cmd.Parameters.AddWithValue("@FirstName", profile.FirstName);
        cmd.Parameters.AddWithValue("@LastName", profile.LastName);
        cmd.Parameters.AddWithValue("@Age", profile.Age);
        cmd.Parameters.AddWithValue("@Gender", profile.Gender);
        cmd.Parameters.AddWithValue("@UserName", profile.UserName);
        cmd.Parameters.AddWithValue("@Password", profile.Password);

        cmd.ExecuteNonQuery();
        return true;
    }
}
