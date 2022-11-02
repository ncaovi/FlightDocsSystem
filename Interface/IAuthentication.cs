using FlightDocsSystem.Model;
using FlightDocsSystem.Model.ViewModel;
using System.Threading.Tasks;

namespace FlightDocsSystem.Interface
{
    public interface IAuthentication
    {
        Task<UserModel> Login(ViewLogin viewLogin);

        Task<UserModel> GetUserEmail(ViewLogin viewLogin);
        
        Task<int> ChangePassword(string email, UserModel userModel);

        

    }
}
