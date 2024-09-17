using UITemplate.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;
using UITemplate.UI.Areas.Admin.Models;
using Newtonsoft.Json;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly BaseService<UserDTO> _userService;
		private readonly BaseService<RoleDTO> _roleService;

		public HomeController(BaseService<UserDTO> userService, BaseService<RoleDTO> roleService)
		{
			_userService = userService;
			_roleService = roleService;
		}

		[HttpGet("/admin/anasayfa")]
		public async Task<IActionResult> Index()
		{
			UserDTO user = new UserDTO();
			RoleDTO role = new RoleDTO();

			var sessionRoles = JsonConvert.DeserializeObject<List<UserRoleDTO>>(HttpContext.Session.GetString("Roles"));
			var roleNames = string.Join(",", sessionRoles.Select(x => x.RoleName));

			if (!(roleNames.Contains("Admin")))
			{
				return View();
			}

			var users = await _userService.PostAsyncList("GetAllUsers", user, true);
			var roles = await _roleService.PostAsyncList("GetAllRoles", role, true);

			HomeViewModel homeViewModel = new HomeViewModel()
			{
				Users = users.Data,
				Roles = roles.Data
			};

			return View(homeViewModel);
		}
	}
}