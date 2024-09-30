using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;

namespace Services;


public class csAttractionServiceDb : IAttractionService
{
    public List<IAttraction> ReadAttractions(int _count)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions.Take(_count).ToList<IAttraction>();
            return attractions;
        }
        
        // var _seeder = new csSeedGenerator();
        // var Attractions = _seeder.ItemsToList<csAttraction>(_count);

        // foreach (var attraction in Attractions){

        //     attraction.Address = new csAddress().Seed(_seeder);

        //     attraction.Comments = _seeder.ItemsToList<csComment>(3).ToList<IComment>();
        //     foreach (var comment in attraction.Comments)
        //     {
        //         var _user = new csUser().Seed(_seeder);
        //         comment.User = _user;
        //     }
            
        // }

        // return Attractions;
    }

    public IAttraction ReadAttraction(Guid _id) => throw new NotImplementedException();
    public List<IAttraction> ReadAttractionsByCity(string _city) => throw new NotImplementedException();
    public List<IAttraction> ReadAttractionsWithoutComments() => throw new NotImplementedException();
    public IAttraction AddAttraction() => throw new NotImplementedException();
    public IAttraction UpdateAttraction(Guid _id) => throw new NotImplementedException();
    public IAttraction RemoveAttraction(Guid _id) => throw new NotImplementedException();

}