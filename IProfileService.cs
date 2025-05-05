using RPT.Models;
namespace RPT.Services
{
    public interface IProfileService
    {
    Profile GetProfileById(int id);
    //void CreateProfile(Profile profile);
    bool UpdateProfile(Profile profile);
    }
}