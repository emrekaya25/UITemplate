using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;

namespace UITemplate.UI.Areas.Admin.Models
{
	public class HomeViewModel
	{
        public List<UserDTO> Users { get; set; }
        public List<RoleDTO> Roles { get; set; }
    }
}
