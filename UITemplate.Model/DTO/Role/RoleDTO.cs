using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.Model.DTO.Role
{
	public class RoleDTO
	{
        public RoleDTO()
        {
            
        }
		public int Id { get; set; } = 0;
		public Guid Guid { get; set; } = Guid.Empty;
		public string Name { get; set; } = "string";
		public List<UserRoleDTO> UserRoles { get; set; } = new();
	}
}
