using RPT.Models;
namespace RPT.Services
{
    public interface ILoginService
    {
         Profile? Login(string username, string password);
    }
}