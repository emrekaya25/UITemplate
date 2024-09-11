using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITemplate.Model.DTO.UserRole
{
	public class UserRoleDTO
	{
        public UserRoleDTO()
        {
            
        }
		public int Id { get; set; } = 0;
		public int UserId { get; set; } = 0;
		public string UserName { get; set; } = "string";
		public int RoleId { get; set; } = 0;
		public string RoleName { get; set; } = "string";
	}
}
