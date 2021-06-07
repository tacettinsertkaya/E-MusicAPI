using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class ResetPassword
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
    }
}
