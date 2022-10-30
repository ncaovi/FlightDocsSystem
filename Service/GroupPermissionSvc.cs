using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace FlightDocsSystem.Service
{
    public class GroupPermissionSvc : IGroupPermission
    {
        protected DataContext _context;
        public GroupPermissionSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddGroupPermissionAsync(GroupPermission GroupPermissions)
        {
            _context.Add(GroupPermissions);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditGroupPermissionAsync(int id, GroupPermission GroupPermissions)
        {
            _context.GroupPermissions.Update(GroupPermissions);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupPermission>> GetGroupPermissionAllAsync()
        {
            var dataContext = _context.GroupPermissions;
            return await dataContext.ToListAsync();
        }

        public async Task<GroupPermission> GetGroupPermissionAsync(int? id)
        {
            var GroupPermissions = await _context.GroupPermissions
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (GroupPermissions == null)
            {
                return null;
            }

            return GroupPermissions;
        }

    }
}