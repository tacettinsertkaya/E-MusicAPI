using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Entity
{
    public class Music:Base
    {
        public string Name { get; set; }

        public string CoverImage { get; set; }
        public string CoverUrl { get; set; }
        public string OwnerFullName { get; set; }

        public string Composition { get; set; }
        public int  Year { get; set; }

    }
}
