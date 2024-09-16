using Microsoft.AspNetCore.Mvc;
using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.UserRole;
using UITemplate.UI.Areas.Admin.Models;
using UITemplate.UI.Services;

namespace UITemplate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly BaseService<RoleDTO> _roleService;
        private readonly BaseService<UserRoleDTO> _userRoleService;

        public RoleController(BaseService<UserRoleDTO> userRoleService, BaseService<RoleDTO> roleService)
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        [HttpGet("/admin/roles")]
        public async Task<IActionResult> Index()
        {
            UserRoleDTO userRoleDTO = new UserRoleDTO();
            var userRoles = await _userRoleService.PostAsyncList("GetAllUserRoles",userRoleDTO,true);
            RoleDTO roleDTO = new RoleDTO();
            var roles = await _roleService.PostAsyncList("GetAllRoles",roleDTO,true);

            RoleViewModel roleViewModel = new RoleViewModel()
            {
                Roles = roles.Data,
                UserRoles = userRoles.Data
            };

            return View(roleViewModel);
        }

        [HttpPost("/admin/addRole")]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            var responseObject = await _roleService.PostAsync("AddRole",roleDTO,true);
            if (responseObject.Data == null)
            {
                TempData["roleResponseError"] = $"{responseObject.Message}";
            }
            else
            {
                TempData["roleResponseSuccess"] = $"{responseObject.Message}";
            }
            return RedirectToAction("Index","Role");
        }

        [HttpPost("/admin/deleteRole")]
        public async Task<IActionResult> DeleteRole(RoleDTO roleDTO)
        {
            var responseObject = await _roleService.PostAsync("DeleteRole",roleDTO,true);
            if (responseObject.Data == null)
            {
                TempData["roleResponseError"] = $"{responseObject.Message}";
            }
            else
            {
                TempData["roleResponseSuccess"] = $"{responseObject.Message}";
            }
            return RedirectToAction("Index", "Role");
        }

        [HttpPost("/admin/UpdateRole")]
        public async Task<IActionResult> UpdateRole(RoleDTO roleDTO)
        {
            var responseObject = await _roleService.PostAsync("UpdateRole",roleDTO,true);
            if (responseObject.Data == null)
            {
                TempData["roleResponseError"] = $"{responseObject.Message}";
            }
            else
            {
                TempData["roleResponseSuccess"] = $"{responseObject.Message}";
            }
            return RedirectToAction("Index","Role");
        }
    }
}
