using Forms.Mobile.Models;
using System.Threading.Tasks;

namespace Forms.Mobile.Services
{
    public interface IAuthenticationService
    {
        Task<UserModel> Login(LoginModel loginModel);
    }
}
