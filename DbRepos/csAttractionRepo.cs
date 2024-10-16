using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;
using Models.DTO;
using System.Data;

namespace DbRepos;

public class csAttractionRepo : IAttractionRepo
{
    //read all attractions. Filtering is possible
   public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //include address and comments, but not users
            var attractions = db.Attractions
            .Include(a => a.AddressDbM)
            .Include(a => a.CommentsDbM)
            .Where(a => a.Category.ToLower().Contains(_category))
            .Where(a => a.AttractionName.ToLower().Contains(_attractionName))
            .Where(a => a.Description.ToLower().Contains(_description))
            .Where(a => a.AddressDbM.Country.ToLower().Contains(_country))
            .Where(a => a.AddressDbM.City.ToLower().Contains(_city))
            .Take(_count).ToList<IAttraction>();

            return attractions;
        }
    }

    //read one attraction
    public IAttraction ReadAttraction(Guid _id) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //including comments, users, address and attractions that share the same address
            var attraction = db.Attractions
            .Include(a => a.CommentsDbM)
            .ThenInclude(c => c.UserDbM)
            .Include(a => a.AddressDbM)
            .ThenInclude(addr => addr.AttractionsDbM)
            .Where(a => a.AttractionId == _id).FirstOrDefault();
                                                                         
            return attraction;
        }
    }

    //filter by city
    public List<IAttraction> ReadAttractionsByCity(string _city) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions
            .Include(a => a.AddressDbM)
            .Where(a => a.AddressDbM.City == _city)
            .ToList<IAttraction>();
            return attractions; 
        }
    }

    //read attractions that have no comments
    public List<IAttraction> ReadAttractionsWithoutComments() 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var attractions = db.Attractions
            .Include(a => a.AddressDbM)
            .Include(a => a.CommentsDbM)
            .Where(a => a.CommentsDbM.Count == 0)
            .ToList<IAttraction>();
            return attractions; 
        }
    }
    
    //add new attraction
    public IAttraction AddAttraction(csAttractionCUdto itemDto)
    {
        if (itemDto.AttractionId != null)
            throw new ArgumentException($"{nameof(itemDto.AttractionId)} must be null when creating a new object");

        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //transfer any changes from DTO to database objects
            //Update individual properties Attraction
            var _item = new csAttractionDbM(itemDto);

            //Update navigation properties
            navProp_csAttractionCUdto_to_csAttractionDbM(db, itemDto, _item);

            //write to database model
            db.Attractions.Add(_item);

            //write to database
            db.SaveChanges();
            
            //return the updated item in non-flat mode
            return ReadAttraction(_item.AttractionId);   
        }
    }

    //update attraction
    public IAttraction UpdateAttraction(csAttractionCUdto itemDto)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //Find the instance with matching id and read the navigation properties.
            var _query1 = db.Attractions
                .Where(a => a.AttractionId == itemDto.AttractionId);
            var _item = _query1
                .Include(a => a.AddressDbM)
                .Include(a => a.CommentsDbM)
                .FirstOrDefault<csAttractionDbM>();

            //If the item does not exists
            if (_item == null) throw new ArgumentException($"Item {itemDto.AttractionId} is not existing");

            //transfer any changes from DTO to database objects
            //Update individual properties
            _item.UpdateFromDTO(itemDto);

            //Update navigation properties
            navProp_csAttractionCUdto_to_csAttractionDbM(db, itemDto, _item);

            //write to database model
            db.Attractions.Update(_item);

            //write to database
            db.SaveChanges();

            //return the updated item in non-flat mode
            return ReadAttraction(_item.AttractionId);  
        }
    }

    //remove attraction
    public adminInfoDbDto RemoveAttraction(Guid _id) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Attractions.RemoveRange(db.Attractions.Where(a => a.AttractionId == _id));
            
            //changeTracker info
            int nrRemovedAttractions = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Deleted);
            var _info = new adminInfoDbDto();
            _info.nrRemovedAttractions = nrRemovedAttractions;

            db.SaveChanges();

            return _info;
        }
    }

    //from all id's in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst
    //Error is thrown if no object is found correspodning to an id.
    private static void navProp_csAttractionCUdto_to_csAttractionDbM(csMainDbContext db, csAttractionCUdto _itemDtoSrc, csAttractionDbM _itemDst)
    {
        //update AddressDbM from itemDto.AddressId
        _itemDst.AddressDbM = (_itemDtoSrc.AddressId != null) ? db.Addresses.FirstOrDefault(
            a => (a.AddressId == _itemDtoSrc.AddressId)) : null;

        //update CommentsDbM from itemDto.CommentsId list
        List<csCommentDbM> _Comments = null;
        if (_itemDtoSrc.CommentsId != null)
        {
            _Comments = new List<csCommentDbM>();
            foreach (var id in _itemDtoSrc.CommentsId)
            {
                var c = db.Comments.FirstOrDefault(i => i.CommentId == id);
                if (c == null)
                    throw new ArgumentException($"Item id {id} not existing");

                _Comments.Add(c);
            }
        }
        _itemDst.CommentsDbM = _Comments;
    }
}
