using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Wrappers
{
    public class ConfirmCodeResponse
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
