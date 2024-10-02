using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;

namespace DbRepos;

public class csSeedRepo : ISeedRepo
{

    //seed 1000 attractions with 0-20 comments each
    public void SeedTestdata()
    {
        var _seeder = new csSeedGenerator();
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //attractions to list
            var _attractions = _seeder.ItemsToList<csAttractionDbM>(1000);
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
            db.SaveChanges();
        }
        
    }

    public void RemoveAllTestdata() => throw new NotImplementedException();

}