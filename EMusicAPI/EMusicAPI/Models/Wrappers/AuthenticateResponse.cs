using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Wrappers
{
    public class AuthenticateResponse
    {
        
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string ImageUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string CompanyId { get; set; }
        public List<string> Roles { get; set; }
    

    }
}
