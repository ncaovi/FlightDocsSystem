using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Service;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;

namespace FlightDocsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermission _groupPermission;
        private readonly DataContext _context;
        public GroupPermissionController(DataContext context, IGroupPermission groupPermission)
        {
            _context = context;
            _groupPermission = groupPermission;
        }
        [HttpPost]
        public async Task<ActionResult<int>> AddGroup(GroupPermission group)
        {
            try
            {
                await _groupPermission.AddGroupPermissionAsync(group);
            }
            catch (Exception ex)
            {

            }
            return Ok(new
            {
                retCode = 1,
                retText = "Thêm thành công"
            });
        }
        [HttpGet]
        [Route("ListGroup")]
        public async Task<ActionResult<IEnumerable<GroupPermission>>> GetGroupAllAsync()
        {
            return await _groupPermission.GetGroupPermissionAllAsync();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, GroupPermission group)
        {
            if (id != group.GroupId)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _groupPermission.EditGroupPermissionAsync(id, group);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(
                 new
                 {
                     retCode = 1,
                     retText = "Sửa thành công"
                 });
        }
        private bool GroupExists(int id)
        {
            return _context.GroupPermissions.Any(e => e.GroupId == id);

        }
        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteGroup(int id)
        {
            var Group = await _context.GroupPermissions.FindAsync(id);
            if (Group == null)
            {
                return NotFound();
            }

            _context.GroupPermissions.Remove(Group);
            await _context.SaveChangesAsync();

            return Ok(
                 new
                 {
                     retCode = 1,
                     retText = "Xóa thành công"
                 });
        }
    }
}