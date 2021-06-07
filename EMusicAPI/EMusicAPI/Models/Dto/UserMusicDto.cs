using EMusicAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class UserMusicDto
    {
        public Music Music { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsPurchashing { get; set; }
        public bool IsViewed { get; set; }
    }
}
