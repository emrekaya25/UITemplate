using Newtonsoft.Json;
using UITemplate.Model.DTO.Login;
using UITemplate.Model.DTO.User;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.UI.Controllers
{
	public class SessionHelper
	{
		private readonly ISession _session;

		public SessionHelper(IHttpContextAccessor httpContextAccessor)
		{
			_session = httpContextAccessor.HttpContext.Session;
		}

		// Session'a kullanıcı bilgilerini kaydetme
		public void SetUser(LoginDTO user)
		{
			_session.SetString("Id", user.Id.ToString());
			_session.SetString("Guid", user.Guid.ToString());
			_session.SetString("Token", user.Token);
			_session.SetString("Name", user.Name + " " + user.LastName);
			_session.SetString("Email", user.Email);
			_session.SetString("Image", user.Image);
			_session.SetString("Roles", JsonConvert.SerializeObject(user.Roles));
		}

		// Session'dan kullanıcı bilgilerini alma
		public LoginDTO GetUser()
		{
			var id = _session.GetString("Id");
			var guid = _session.GetString("Guid");
			var token = _session.GetString("Token");
			var name = _session.GetString("Name");
			var email = _session.GetString("Email");
			var image = _session.GetString("Image");
			var rolesJson = _session.GetString("Roles");

			var roles = JsonConvert.DeserializeObject<List<UserRoleDTO>>(rolesJson);


			return new LoginDTO
			{
				Id = Convert.ToInt32(id),
				Guid = Guid.Parse(guid),
				Token = token,
				Name = name,
				Email = email,
				Image = image,
				Roles = roles,
			};
		}

		// Session'da mevcut kullanıcı bilgilerini güncelleme
		public void UpdateUser(UserDTO updatedUser)
		{
			LoginDTO loginDTO = new LoginDTO
			{
				Email = updatedUser.Email,
				Image = updatedUser.Image,
				LastName = updatedUser.LastName,
				Name = updatedUser.Name,
			};
			_session.SetString("Name", loginDTO.Name + " " + loginDTO.LastName);
			_session.SetString("Email", loginDTO.Email);
			_session.SetString("Image", loginDTO.Image);
		}

		// Session'daki bilgileri temizleme
		public void ClearSession()
		{
			_session.Clear();
		}
	}
}
