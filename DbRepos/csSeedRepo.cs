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

    //seed specified or default 1000 attractions with 0-20 comments each
    public adminInfoDbDto SeedTestdata(int _count)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //remove old seeded data first
            RemoveAllTestdata(true);
            var _seeder = new csSeedGenerator();

            //attractions to list
            var _attractions = _seeder.UniqueItemsToList<csAttractionDbM>(_count); 
            //users to list
            var _users = _seeder.UniqueItemsToList<csUserDbM>(50);
            //unique addresses to list
            var _addresses = _seeder.UniqueItemsToList<csAddressDbM>(_count);
            int idx = 0;
            
            foreach (var attraction in _attractions) {
                //add address to attraction
                attraction.AddressDbM = _addresses[idx];
                idx++;
                //add comments to attraction
                attraction.CommentsDbM = _seeder.ItemsToList<csCommentDbM>(_seeder.Next(0, 21));
                //add user to comment
                attraction.CommentsDbM.ForEach(c => c.UserDbM = _seeder.FromList(_users));
            }
            db.Attractions.AddRange(_attractions);
            db.Users.AddRange(_users);
            db.Addresses.AddRange(_addresses);

            //changeTracker info
            int nrSeededAttractions = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Added);
            int nrSeededComments = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csCommentDbM) && entry.State == EntityState.Added);
            int nrSeededUsers = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csUserDbM) && entry.State == EntityState.Added);
            int nrSeededAddresses = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAddressDbM) && entry.State == EntityState.Added);

            var _info = new adminInfoDbDto();
            _info.nrSeededAttractions = nrSeededAttractions;
            _info.nrSeededComments = nrSeededComments;
            _info.nrSeededUsers = nrSeededUsers;
            _info.nrSeededAddresses = nrSeededAddresses;
            
            db.SaveChanges();
            return _info;
        }
        
    }

    //remove all seeded data
    public adminInfoDbDto RemoveAllTestdata(bool seeded) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Attractions.RemoveRange(db.Attractions.Where(a => a.Seeded == seeded));
            db.Comments.RemoveRange(db.Comments.Where(c => c.Seeded == seeded));
            db.Users.RemoveRange(db.Users.Where(u => u.Seeded == seeded));
            db.Addresses.RemoveRange(db.Addresses.Where(a => a.Seeded == seeded));

            //changeTracker info
            int nrRemovedAttractions = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Deleted);
            int nrRemovedComments = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csCommentDbM) && entry.State == EntityState.Deleted);
            int nrRemovedUsers = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csUserDbM) && entry.State == EntityState.Deleted);
            int nrRemovedAddresses = db.ChangeTracker.Entries().Count(
            entry => (entry.Entity is csAddressDbM) && entry.State == EntityState.Deleted);
            
            var _info = new adminInfoDbDto();
            _info.nrRemovedAttractions = nrRemovedAttractions;
            _info.nrRemovedComments = nrRemovedComments;
            _info.nrRemovedUsers = nrRemovedUsers;
            _info.nrRemovedAddresses = nrRemovedAddresses;

            db.SaveChanges();

            return _info;
        }
    }


}