using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.UI.Areas.Admin.Models
{
	public class UserViewModel
	{
        public List<UserDTO> Users { get; set; }
        public List<RoleDTO> Roles { get; set; }
        public List<UserRoleDTO> UserRoles { get; set; }
        public UserDTO User { get; set; }
    }
}
