using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Wrappers
{
    public class Notif
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
