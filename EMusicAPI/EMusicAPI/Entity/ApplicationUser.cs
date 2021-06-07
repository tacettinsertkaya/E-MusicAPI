using Microsoft.AspNetCore.Identity;
using System;

namespace EMusicAPI.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string ConfirmCode { get; set; }
        public string Name { get; set; }
     
        public string Surname { get; set; }

        public string Phone { get; set; }

       
        public DateTime? BirthFullDate { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
