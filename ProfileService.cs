using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;
namespace RPT.Services
{
    public class ProfileService:IProfileService
    {
        private readonly DbService _dbs;

    public ProfileService(DbService dbs)
    {
        _dbs = dbs;
    }

    public Profile GetProfileById(int id)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL GetProfile(@id)", conn);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Profile
            {
                ProfileId = reader.GetInt32("ProfileId"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Age = reader.GetInt32("Age"),
                Gender = reader.GetString("Gender"),
                UserName = reader.GetString("UserName"),
                Password = reader.GetString("Password")
            };
        }
        return null;
    }

    /*public void CreateProfile(Profile profile)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL CreateProfile(@FirstName, @LastName, @Age, @Gender, @UserName, @Password)", conn);
        cmd.Parameters.AddWithValue("@FirstName", profile.FirstName);
        cmd.Parameters.AddWithValue("@LastName", profile.LastName);
        cmd.Parameters.AddWithValue("@Age", profile.Age);
        cmd.Parameters.AddWithValue("@Gender", profile.Gender);
        cmd.Parameters.AddWithValue("@UserName", profile.UserName);
        cmd.Parameters.AddWithValue("@Password", profile.Password);
        cmd.ExecuteNonQuery();
    }*/

    public bool UpdateProfile(Profile profile)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL UpdateProfile(@ProfileId, @FirstName, @LastName, @Age, @Gender, @UserName, @Password)", conn);
        cmd.Parameters.AddWithValue("@ProfileId", profile.ProfileId);
        cmd.Parameters.AddWithValue("@FirstName", profile.FirstName);
        cmd.Parameters.AddWithValue("@LastName", profile.LastName);
        cmd.Parameters.AddWithValue("@Age", profile.Age);
        cmd.Parameters.AddWithValue("@Gender", profile.Gender);
        cmd.Parameters.AddWithValue("@UserName", profile.UserName);
        cmd.Parameters.AddWithValue("@Password", profile.Password);

        int affectedRows = cmd.ExecuteNonQuery();
        return affectedRows > 0;
    }
    }
}