using FlightDocsSystem.Model;
using FlightDocsSystem.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FlightDocsSystem.Interface
{
    public interface IAdmin
    {
        #region Role
        Task<int> AddRole(RoleModel roleModel);
        Task<List<RoleModel>> GetRole();
        Task<int> EditRole(RoleModel roleModel);
        Task<int> DeleteRole(int id);
        #endregion


        public Task<List<UserModel>> GetUserAllAsync();
        public Task<bool> EditUserAsync(int id, UserModel users);
        public Task<bool> AddUserAsync(UserModel users);
        public Task<UserModel> GetUserAsync(int? id);
        Task<bool> isEmail(string email);

        //public Task<bool> DeleteUserAsync(int id, User User);
        public Task<UserModel> Login(ViewLogin viewLogin);
        
        Task<int> ChangePasswordCode(string email, UserModel user);
    }
}
