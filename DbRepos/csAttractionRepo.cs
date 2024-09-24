using Microsoft.EntityFrameworkCore;

using Configuration;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;

namespace DbRepos;

public class csAttractionRepo : IAttractionRepo
{
    public async Task<List<csAttractionDbM>> ListAttractions(int _count)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            // return await db.Animals.Include(a => a.ZooDbM).Take(_count).ToListAsync(); 
            throw new NotImplementedException();
        }
    }
    public async Task Seed(int _count)
    {
        var _seeder = new csSeedGenerator();
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            // var zoos = _seeder.ItemsToList<csZooDbM>(5);
            // var animals = _seeder.ItemsToList<csAnimalDbM>(_count);

            // foreach (var a in animals)
            // {
            //     a.ZooDbM = _seeder.FromList(zoos);
            // }
            
            
            // db.Animals.AddRange(animals);
            // await db.SaveChangesAsync();
        }
    }
}
