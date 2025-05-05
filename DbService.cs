using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;

namespace RPT.Services;

public class DbService
{
    private readonly string _connectionString;

    public DbService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
