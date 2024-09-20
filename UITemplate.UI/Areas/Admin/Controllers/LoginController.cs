using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using UITemplate.Model.DTO.Login;
using UITemplate.Model.DTO.UserRole;
using UITemplate.Model.Result;
using UITemplate.UI.Controllers;
using UITemplate.UI.Services;

namespace UITemplate.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly BaseService<LoginDTO> _loginService;
        private readonly SessionHelper _sessionHelper;

		public LoginController(BaseService<LoginDTO> loginService, SessionHelper sessionHelper)
		{
			_loginService = loginService;
			_sessionHelper = sessionHelper;
		}

		[HttpGet("/admin/login")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
			var url = "https://localhost:7272/api/Login";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			var body = JsonConvert.SerializeObject(loginDTO);
			request.AddBody(body, "application/json");

			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonConvert.DeserializeObject<ApiResponse<LoginDTO>>(response.Content);

			if (responseObject.StatusCode == (int)HttpStatusCode.OK)
            {
                _sessionHelper.SetUser(responseObject.Data);

                var roleNames = JsonConvert.DeserializeObject<List<UserRoleDTO>>(HttpContext.Session.GetString("Roles"));
                var roles = string.Join(",", roleNames.Select(x=>x.RoleName));

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("GetUser","User", new { userGuid = responseObject.Data.Guid });
                }
                
            }
            else
            {
				TempData["LoginError"] = $"{responseObject.Message}";
				return RedirectToAction("Index", "Login");
			}
        }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}
