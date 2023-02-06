using System;

namespace EMusicAPI.Entity
{
    public class UserMusic:Base
    {
        public bool IsFavorite { get; set; }
        public bool IsPurchashing { get; set; }
        public bool IsViewed { get; set; }
        public string  MusicId { get; set; }

        public virtual Music Music { get; set; }
    }
}
