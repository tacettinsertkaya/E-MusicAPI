using EMusicAPI.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Context
{
    public class EMusicDbContext : DbContext
    {
        public EMusicDbContext(DbContextOptions<EMusicDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Music> Musics { get; set; }
        public DbSet<UserMusic> UserMusic { get; set; }





    }
}
