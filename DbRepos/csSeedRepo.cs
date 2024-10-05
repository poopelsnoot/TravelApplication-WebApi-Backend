using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;
using Microsoft.Extensions.Logging;
using Models.DTO;
using System.Data;

namespace DbRepos;

public class csSeedRepo : ISeedRepo
{

    //seed 1000 attractions with 0-20 comments each
    public adminInfoDbDto SeedTestdata()
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            RemoveAllTestdata(true);
            var _seeder = new csSeedGenerator();

            //attractions to list. Works good with 100, but i get error when i try with 1000
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
            
            db.SaveChanges();
            return _info;
        }
        
    }

    public adminInfoDbDto RemoveAllTestdata(bool seeded) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Attractions.RemoveRange(db.Attractions.Where(a => a.Seeded == seeded));
            db.Comments.RemoveRange(db.Comments.Where(c => c.Seeded == seeded));
            db.Users.RemoveRange(db.Users.Where(u => u.Seeded == seeded));
            db.Addresses.RemoveRange(db.Addresses.Where(a => a.Seeded == seeded));

            int nrRemovedAttractions = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Deleted);

            var _info = new adminInfoDbDto();
            _info.nrRemovedAttractions = nrRemovedAttractions;

            db.SaveChanges();

            return _info;
        }
    }


}