using System.Data;
using MySql.Data.MySqlClient;
using RPT.Models;

namespace RPT.Services;

public class GoalService:IGoalService
{
    private readonly DbService _dbs;

    public GoalService(DbService dbs)
    {
        _dbs = dbs;
    }

    public Goal GetGoalById(int id)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL GetGoal(@GoalId)", conn);
        cmd.Parameters.AddWithValue("@GoalId", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Goal
            {
                GoalId = reader.GetInt32("GoalId"),
                ProfileId = reader.GetInt32("ProfileId"),
                CurrentAge = reader.GetInt32("CurrentAge"),
                RetirementAge = reader.GetInt32("RetirementAge"),
                TargetSavings = reader.GetDecimal("TargetSavings"),
                MonthlyContribution = reader.GetDecimal("MonthlyContribution"),
                CurrentSavings = reader.GetDecimal("CurrentSavings")
            };
        }
        return null;
    }
    public bool GoalExists(int profileId)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("SELECT COUNT(1) FROM Goals WHERE ProfileId = @ProfileId", conn);
        cmd.Parameters.AddWithValue("@ProfileId", profileId);

        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }
    public bool CreateGoal(Goal goal)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL CreateGoal(@ProfileId, @CurrentAge, @RetirementAge, @TargetSavings, @MonthlyContribution, @CurrentSavings)", conn);
        cmd.Parameters.AddWithValue("@ProfileId", goal.ProfileId);
        cmd.Parameters.AddWithValue("@CurrentAge", goal.CurrentAge);
        cmd.Parameters.AddWithValue("@RetirementAge", goal.RetirementAge);
        cmd.Parameters.AddWithValue("@TargetSavings", goal.TargetSavings);
        cmd.Parameters.AddWithValue("@MonthlyContribution", goal.MonthlyContribution);
        cmd.Parameters.AddWithValue("@CurrentSavings", goal.CurrentSavings);

        int rowsAffected = cmd.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public bool UpdateGoal(Goal goal)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL UpdateGoal(@GoalId, @ProfileId, @CurrentAge, @RetirementAge, @TargetSavings, @MonthlyContribution, @CurrentSavings)", conn);
        cmd.Parameters.AddWithValue("@GoalId", goal.GoalId);
        cmd.Parameters.AddWithValue("@ProfileId", goal.ProfileId);
        cmd.Parameters.AddWithValue("@CurrentAge", goal.CurrentAge);
        cmd.Parameters.AddWithValue("@RetirementAge", goal.RetirementAge);
        cmd.Parameters.AddWithValue("@TargetSavings", goal.TargetSavings);
        cmd.Parameters.AddWithValue("@MonthlyContribution", goal.MonthlyContribution);
        cmd.Parameters.AddWithValue("@CurrentSavings", goal.CurrentSavings);

        int affectedRows = cmd.ExecuteNonQuery();
        return affectedRows > 0;
    }

    public bool DeleteGoal(int id)
    {
        using var conn = _dbs.GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("CALL DeleteGoal(@GoalId)", conn);
        cmd.Parameters.AddWithValue("@GoalId", id);

        int affectedRows = cmd.ExecuteNonQuery();
        return affectedRows > 0;
    }
}
