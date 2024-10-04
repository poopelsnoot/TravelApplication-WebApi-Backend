using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;
using Microsoft.Extensions.Logging;
using Models.DTO;

namespace DbRepos;

public class csSeedRepo : ISeedRepo
{

    //seed 1000 attractions with 0-20 comments each
    public async Task<adminInfoDbDto> SeedTestdata()
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            await RemoveAllTestdata(true);
            var _seeder = new csSeedGenerator();

            //attractions to list
            var _attractions = _seeder.ItemsToList<csAttractionDbM>(100);
            //users to list
            var _users = _seeder.ItemsToList<csUserDbM>(50);
            
            //add address and comments to attraction
            foreach (var attraction in _attractions) {
                attraction.AddressDbM = new csAddressDbM().Seed(_seeder);
                attraction.CommentsDbM = _seeder.ItemsToList<csCommentDbM>(_seeder.Next(0, 21));

                //add user to comment
                foreach (var comment in attraction.CommentsDbM)
                {
                    comment.UserDbM = _seeder.FromList(_users);
                }

            }
            db.Attractions.AddRange(_attractions);

            int nrSeededAttractions = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Added);

            var _info = new adminInfoDbDto();
            _info.nrSeededAttractions = nrSeededAttractions;
            
            await db.SaveChangesAsync();
            return _info;
        }
        
    }

    public async Task<adminInfoDbDto> RemoveAllTestdata(bool seeded) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Attractions.RemoveRange(db.Attractions.Where(a => a.Seeded == seeded));

            int nrSeededAttractions = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Deleted);

            var _info = new adminInfoDbDto();
            _info.nrSeededAttractions = nrSeededAttractions;

            await db.SaveChangesAsync();

            return _info;
        }
    }


}