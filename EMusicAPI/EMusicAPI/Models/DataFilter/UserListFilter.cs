using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.DataFilter
{
    public class UserListFilter : PaginationFilter
    {
        public UserListFilter(int pageNumber, int pageSize)
        {
            base.PageNumber = pageNumber;
            base.PageSize = pageSize;
        }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        
    }
}
