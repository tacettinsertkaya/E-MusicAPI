using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "User Id is required")]
        public string Id { get; set; }


        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }


        [Phone]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }


        public DateTime? BirthFullDate { get; set; }

        public Guid LanguageId { get; set; }
        public Guid TimezoneId { get; set; }
    }
}
