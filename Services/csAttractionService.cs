// using Microsoft.Extensions.Logging;
// using Models;
// using Seido.Utilities.SeedGenerator;

// namespace Services;


// public class csAttractionService : IAttractionService
// {
//     public List<IAttraction> ReadAttractions(int _count)
//     {
    
//         var _seeder = new csSeedGenerator();
//         var Attractions = _seeder.ItemsToList<csAttraction>(_count).ToList<IAttraction>();

//         foreach (var attraction in Attractions){

//             attraction.Address = new csAddress().Seed(_seeder);

//             attraction.Comments = _seeder.ItemsToList<csComment>(3).ToList<IComment>();
//             foreach (var comment in attraction.Comments)
//             {
//                 var _user = new csUser().Seed(_seeder);
//                 comment.User = _user;
//             }
            
//         }

//         return Attractions;
//     }
// }