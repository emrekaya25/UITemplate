using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.UI.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public List<RoleDTO> Roles { get; set; }
        public List<UserRoleDTO> UserRoles { get; set; }
    }
}
