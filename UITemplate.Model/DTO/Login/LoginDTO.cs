using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITemplate.Model.DTO.UserRole;

namespace UITemplate.Model.DTO.Login
{
    public class LoginDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "string";
        public string LastName { get; set; } = "string";
        public string Email { get; set; } = "string";
        public string Password { get; set; } = "string";
        public List<UserRoleDTO> Roles { get; set; } = new();
        public string Token { get; set; } = "string";
    }
}
