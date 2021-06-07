using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Entity
{
    public class UserMusic:Base
    {
        public bool IsFavorite { get; set; }
        public bool IsPurchashing { get; set; }
        public bool IsViewed { get; set; }
        public Guid  MusicId { get; set; }

        public virtual Music Music { get; set; }
    }
}
