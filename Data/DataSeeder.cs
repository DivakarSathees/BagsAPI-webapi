using BagAPI.Models;

namespace BagAPI.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Bags.Any())
            {
                var bags = new[]
        {
            new Bag { Id = 1, Name = "Backpack", Weight = 2.5, Capacity = 45 },
            new Bag { Id = 2, Name = "ToteBag", Weight = 1.8, Capacity = 30 },
            new Bag { Id = 3, Name = "DuffelBag", Weight = 3.2, Capacity = 50 },
            new Bag { Id = 4, Name = "SlingBag", Weight = 1.2, Capacity = 20 },
            new Bag { Id = 5, Name = "HikingBag", Weight = 3.8, Capacity = 60 },
            new Bag { Id = 6, Name = "Backpack", Weight = 1.5, Capacity = 25 },
            new Bag { Id = 7, Name = "Backpack", Weight = 4.0, Capacity = 70 },
            new Bag { Id = 8, Name = "ToteBag", Weight = 1.0, Capacity = 15 },
            new Bag { Id = 9, Name = "DuffelBag", Weight = 3.5, Capacity = 50 },
            new Bag { Id = 10, Name = "GymBag", Weight = 2.0, Capacity = 35 }
        };
            

                _context.Bags.AddRange(bags);
                _context.SaveChanges();
            }
        }
    }
}
