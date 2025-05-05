using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;

namespace RPT.Services;

public class FinancialYearDataService:IFinancialYearDataService
{
    private readonly DbService _dbs;

    public FinancialYearDataService(DbService dbs)
    {
        _dbs = dbs;
    }

    public FinancialYearData GetFinancialYearDataById(int profileId)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL GetFinancialYearDataById(@ProfileId)", conn);
        cmd.Parameters.AddWithValue("@ProfileId", profileId);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new FinancialYearData
            {
                Id = reader.GetInt32("Id"),
                ProfileId = reader.GetInt32("ProfileId"),
                Year = reader.GetInt32("Year"),
                MonthlyInvestment = reader.GetDecimal("MonthlyInvestment")
            };
        }
        return null;
    }

    public bool CreateFinancialYearData(FinancialYearData financialData)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL CreateFinancialYearData(@ProfileId, @Year, @MonthlyInvestment)", conn);
        cmd.Parameters.AddWithValue("@ProfileId", financialData.ProfileId);
        cmd.Parameters.AddWithValue("@Year", financialData.Year);
        cmd.Parameters.AddWithValue("@MonthlyInvestment", financialData.MonthlyInvestment);
        cmd.ExecuteNonQuery();

         int rowsAffected = cmd.ExecuteNonQuery();
         return rowsAffected > 0; 
    }

    public bool UpdateFinancialYearData(FinancialYearData financialData)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL UpdateFinancialYearData(@Id, @ProfileId, @Year, @MonthlyInvestment)", conn);
        cmd.Parameters.AddWithValue("@Id", financialData.Id);
        cmd.Parameters.AddWithValue("@ProfileId", financialData.ProfileId);
        cmd.Parameters.AddWithValue("@Year", financialData.Year);
        cmd.Parameters.AddWithValue("@MonthlyInvestment", financialData.MonthlyInvestment);

        int affectedRows = cmd.ExecuteNonQuery();
        return affectedRows > 0;
    }

    public bool DeleteFinancialYearData(int id)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL DeleteFinancialYearData(@Id)", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        int affectedRows = cmd.ExecuteNonQuery();
        return affectedRows > 0;
    }
}
