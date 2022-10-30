using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;
using FlightDocsSystem.Model.ViewModel;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDocsSystem.Service
{
    public class AuthenticationSvc : IAuthentication
    {
        protected DataContext _context;
        protected IEncode _enCode;

        public AuthenticationSvc(DataContext context, IEncode encode)
        {
            _context = context;
            _enCode = encode;
        }

        public async Task<UserModel> Login(ViewLogin viewLogin)
        {

            var admin = await _context.UserModels.Where(
                p => p.UserEmail.Equals(viewLogin.UserEmail) && p.UserPassword.Equals(_enCode.Encode(viewLogin.UserPassword))
                ).FirstOrDefaultAsync();
            return admin;
        }

        public async Task<UserModel> GetUserEmail(ViewLogin viewLogin)
        {
            UserModel user = null;
            user = await _context.UserModels.FirstOrDefaultAsync(u => u.UserEmail == viewLogin.UserEmail);
            return user;
        }
        public async Task<UserModel> GetUserEmail(string email)
        {
            UserModel users = null;
            users = await _context.UserModels.FirstOrDefaultAsync(u => u.UserEmail == email);
            return users;
        }



        public async Task<int> ChangePassword(string email, UserModel userModel)
        {
            int ret = 0;
            try
            {

                UserModel _user = null;
                _user = await GetUserEmail(email);


                _user.UserPassword = _enCode.Encode(userModel.UserPassword);
                _context.Update(_user);
                await _context.SaveChangesAsync();

                ret = userModel.UserId;
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
    }
}
