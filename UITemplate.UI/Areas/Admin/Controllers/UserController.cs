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

			var users = await _userService.PostAsyncList("GetAllUsers",userDTO,false);
			var roles = await _roleService.PostAsyncList("GetAllRoles",roleDTO,false);
			var userRoles = await _userRoleService.PostAsyncList("GetAllUserRoles",userRoleDTO,false);

			UserViewModel userViewModel = new UserViewModel()
			{
				Roles = roles,
				UserRoles = userRoles,
				Users = users
			};
			return View(userViewModel);
		}

		[HttpPost("/admin/addUser")]
		public async Task AddUser(UserDTO userDTO)
		{
			await _userService.PostAsync("AddUser",userDTO,true);
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
