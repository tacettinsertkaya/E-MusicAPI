using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Models.Dto
{
    public class BaseDto
    {
        public string id { get; set; }
        public string userId { get; set; }
        public DateTime updateDate { get; set; } = DateTime.Now;
        public DateTime createDate { get; set; } = DateTime.Now;
        public bool status { get; set; } = true;
    }
}
