using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models;
using FlightDocsSystem.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using FlightDocsSystem.Model;
using FlightDocsSystem.Interface;

namespace FlightDocsSystem.Service
{
    public class AdminSvc : IAdmin
    {
        protected DataContext _context;
        public AdminSvc(DataContext context)
        {
            _context = context;
        }


        #region Role
        public async Task<List<RoleModel>> GetRole()
        {
            var role = await _context.RoleModels.ToListAsync();
            return role;
        }
        public async Task<RoleModel> roleId(int id)
        {

            var role = await _context.RoleModels.FindAsync(id);
            return role;
        }
        public async Task<int> EditRole(RoleModel roleModel)
        {
            int ret = 0;
            try
            {
                RoleModel role = null;
                role = await roleId(roleModel.RoleId);
                role.RoleName = roleModel.RoleName;

                _context.Update(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        public async Task<int> AddRole(RoleModel roleModel)
        {
            int ret = 0;
            try
            {
                _context.Add(roleModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> DeleteRole(int id)
        {
            int ret = 0;
            try
            {
                var role = await roleId(id);
                _context.Remove(role);
                await _context.SaveChangesAsync();
                ret = role.RoleId;
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
        #endregion
        public async Task<bool> AddUserAsync(UserModel users)
        {
            _context.Add(users);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditUserAsync(int id, UserModel users)
        {
            _context.UserModels.Update(users);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserModel>> GetUserAllAsync()
        {
            var dataContext = _context.UserModels;
            return await dataContext.ToListAsync();
        }

        public async Task<UserModel> GetUserAsync(int? id)
        {
            var users = await _context.UserModels
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return null;
            }

            return users;
        }
        public async Task<UserModel> GetUserEmail(string email)
        {
            UserModel users = null;
            users = await _context.UserModels.FirstOrDefaultAsync(u => u.UserEmail == email);
            return users;
        }
        public async Task<bool> isEmail(string email)
        {
            bool ret = false;
            try
            {
                UserModel user = await _context.UserModels.Where(x => x.UserEmail == email).FirstOrDefaultAsync();
                if (user != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<UserModel> Login(ViewLogin login)
        {
            UserModel user = await _context.UserModels.Where(x => x.UserEmail == login.UserEmail
                  && x.UserPassword == (login.UserPassword)).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<int> ChangePasswordCode(string email, UserModel user)
        {
            int ret = 0;
            try
            {

                UserModel _user = null;
                _user = await GetUserEmail(email);


                _user.UserPassword = user.UserPassword;
                _context.Update(_user);
                await _context.SaveChangesAsync();

                ret = user.UserId;
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            return ret;
        }
    }
}