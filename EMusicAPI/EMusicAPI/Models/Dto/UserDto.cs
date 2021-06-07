using EMusicAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class UserDto
    {

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RawPassword { get; set; }
        public string Statu { get; set; }
        public Guid CompanyId { get; set; }
    }
}
