using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class RoleChange
    {
        public string UserId { get; set; }
        public string OldRoleId { get; set; }
        public string NewRoleId { get; set; }
    }
}
