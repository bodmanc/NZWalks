using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficutlites
            // Easy, medium, hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("958cd129-61ac-4b3f-bea7-b5819a48f950"),
                    Name = "Easy"
                },

               new Difficulty()
               {
                    Id = Guid.Parse("6086ba13-bc7d-4e2c-8763-d1d2b3b22e98"),
                    Name = "Medium"
               },

               new Difficulty()
               {
                    Id = Guid.Parse("d353fa46-3058-4c65-8f90-e66ca6cbd0e7"),
                    Name = "Hard"
               }
            };

            // Seed difficulties to db
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            // Seed data for regions
            var regions = new List<Region>()
            {

                new Region()
                {
                    Id = Guid.Parse("b79af93c-93b0-44a6-9b97-3d4e45c1fb6f"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "some-img.jpg"
                },

                new Region()
                {
                    Id = Guid.Parse("4b216e4c-2e43-4252-8e0a-8586cc7e7d40"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "another-img-jpg"
                }
            };
           
            // Seed difficulties to db
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
