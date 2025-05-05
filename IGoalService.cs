using RPT.Models;
namespace RPT.Services;

    public interface IGoalService
    {
    Goal GetGoalById(int id);
    bool GoalExists(int profileId);
    bool CreateGoal(Goal goal);
    bool UpdateGoal(Goal goal);
    bool DeleteGoal(int id);
    }