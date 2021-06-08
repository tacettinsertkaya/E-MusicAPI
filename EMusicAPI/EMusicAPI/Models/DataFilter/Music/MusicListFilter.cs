using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.DataFilter.Music
{
    public class MusicListFilter : PaginationFilter
    {
        public MusicListFilter(int pageNumber, int pageSize)
        {
            base.PageNumber = pageNumber;
            base.PageSize = pageSize;

        }

        public string Name { get; set; }
        public string OwnerFullName { get; set; }
        public string UserId { get; set; }
        public string SearchParams { get; set; }
    }
}
