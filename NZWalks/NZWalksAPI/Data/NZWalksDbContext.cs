using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext: DbContext // this calss inherits from the dbcintext class from the entit core package
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)//send the connection thru program.cs file
        {
            
        }
        //create db set which is a property of dbcontext class that represent a collection of entity in the db
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
