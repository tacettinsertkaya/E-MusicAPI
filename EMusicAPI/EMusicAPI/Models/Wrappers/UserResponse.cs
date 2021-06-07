using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Wrappers
{
    public class UserResponse
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string ImageUrl { get; set; }
        public int UserType { get; set; }

        public DateTime? BirthFullDate { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
