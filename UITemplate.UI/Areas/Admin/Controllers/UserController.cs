using Microsoft.AspNetCore.Mvc;
using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;
using UITemplate.Model.DTO.UserRole;
using UITemplate.UI.Areas.Admin.Models;
using UITemplate.UI.Services;

namespace UITemplate.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly BaseService<UserDTO> _userService;
		private readonly BaseService<RoleDTO> _roleService;
		private readonly BaseService<UserRoleDTO> _userRoleService;

		public UserController(BaseService<UserDTO> userService, BaseService<UserRoleDTO> userRoleService, BaseService<RoleDTO> roleService)
		{
			_userService = userService;
			_userRoleService = userRoleService;
			_roleService = roleService;
		}

		[HttpGet("/admin/users")]
		public async Task<IActionResult> Index()
		{
			UserDTO userDTO = new UserDTO();
			RoleDTO roleDTO = new RoleDTO();
			UserRoleDTO userRoleDTO = new UserRoleDTO();

			var users = await _userService.PostAsyncList("GetAllUsers",userDTO,true);
			var roles = await _roleService.PostAsyncList("GetAllRoles",roleDTO,true);
			var userRoles = await _userRoleService.PostAsyncList("GetAllUserRoles",userRoleDTO,true);

			UserViewModel userViewModel = new UserViewModel()
			{
				Roles = roles.Data,
				UserRoles = userRoles.Data,
				Users = users.Data
			};
			return View(userViewModel);
		}

		[HttpGet("/admin/user")]
		public async Task<IActionResult> GetUser(UserDTO userDTO)
		{
			var user = await _userService.PostAsync("GetUser",userDTO,true);
			return View(user.Data);
		}

		[HttpPost("/admin/addUser")]
		public async Task<IActionResult> AddUser(UserDTO userDTO, int[] userRoles,IFormFile Image)
		{
			userDTO.UserRoles = userRoles.Select(roleId => new UserRoleDTO { RoleId = roleId }).ToList(); //Rolleri liste halinde UserRoles değişkenine atama

			//Fotoğrafı base64 şekline çevirme
            string base64String = null;

            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    base64String = Convert.ToBase64String(fileBytes);
                }
                userDTO.Image = base64String;
            }

            var responseObject = await _userService.PostAsync("AddUser",userDTO,true);
			if (responseObject.Data == null)
			{
                TempData["AddUserError"] = $"{responseObject.Message}";
               
			}
			return RedirectToAction("Index", "User");

        }

		[HttpPost("/admin/deleteUser")]
		public async Task DeleteUser(UserDTO userDTO)
		{
			await _userService.PostAsync("DeleteUser",userDTO,true);
		}

		[HttpPost("/admin/updateUser")]
		public async Task UpdateUser(UserDTO userDTO)
		{
			await _userService.PostAsync("UpdateUser",userDTO,true);
		}

		
	}
}
