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
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _user;
        private readonly DataContext _context;
        public AdminController(DataContext context, IAdmin user)
        {
            _context = context;
            _user = user;
        }

        #region Role

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetRole")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRole()
        {
            var role = await _user.GetRole();
            return role;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddRole")]
        public async Task<ActionResult<int>> AddRole(RoleModel roleModel)
        {
            try
            {
                var id = await _user.AddRole(roleModel);
                roleModel.RoleId = id;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(1);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("EditRole")]
        public async Task<ActionResult<int>> EditRole(RoleModel roleModel)
        {
            try
            {
                var id = await _user.EditRole(roleModel);
                roleModel.RoleId = id;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(1);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteRole/{id}")]
        public async Task<ActionResult<int>> DeleteRole(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _user.DeleteRole(id);

            }
            catch (Exception ex)
            {
                return BadRequest(-1);
            }

            return Ok(1);
        }
        #endregion



        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> PostAsync(UserModel users)
        {
            if (ModelState.IsValid)
            {
                if (await _user.isEmail(users.UserEmail))
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Email đã tồn tại",
                        data = ""
                    });
                }
                else
                {
                    if (await _user.AddUserAsync(users))
                    {
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "Thành công",
                            data = await _user.GetUserAsync(users.UserId)
                        });
                    }
                }

            }
            return Ok(new
            {
                retCode = 0,
                retText = "Thất bại"
            });
        }
        [HttpGet]
        [Route("ListUser")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserAllAsync()
        {

            return await _user.GetUserAllAsync(); ;
        }

        [HttpPut("ChangepassUser/{id}")]
        public async Task<IActionResult> PutUser(int id, UserModel User)
        {
            if (id != User.UserId)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _user.EditUserAsync(id, User);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                //_NguoiDung.GetNguoidungAsync(id),
                retCode = 0,
                retText = "Update thành công"
            });

        }
        private bool UserExists(int id)
        {
            return _context.UserModels.Any(e => e.UserId == id);

        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.UserModels.FindAsync(id);
            if (user == null)
            {
                return NotFound();

            }

            _context.UserModels.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}