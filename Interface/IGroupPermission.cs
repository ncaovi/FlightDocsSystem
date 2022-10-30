using FlightDocsSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightDocsSystem.Interface
{
    public interface IGroupPermission
    {
        public Task<List<GroupPermission>> GetGroupPermissionAllAsync();
        public Task<bool> EditGroupPermissionAsync(int id, GroupPermission GroupPermissions);
        public Task<bool> AddGroupPermissionAsync(GroupPermission GroupPermissions);
        public Task<GroupPermission> GetGroupPermissionAsync(int? id);

    }
}
