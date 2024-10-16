using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbRepos;

public class csUserRepo : IUserRepo
{
    //read all users
    public List<IUser> ReadUsers() 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //include comments
            var user = db.Users
            .Include(u => u.CommentDbM)
            .ToList<IUser>();
                                                                         
            return user;
        }
    }

    //read one user
    public IUser ReadUser(Guid _userId) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //include comments
            var user = db.Users
            .Include(u => u.CommentDbM)
            .Where(a => a.UserId == _userId).FirstOrDefault();
                                                                         
            return user;
        }
    }

    //add new user
    public IUser AddUser(csUserCUdto itemDto)
    {
        if (itemDto.UserId != null)
            throw new ArgumentException($"{nameof(itemDto.UserId)} must be null when creating a new object");

        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            //transfer any changes from DTO to database objects
            //Update individual properties User
            var _item = new csUserDbM(itemDto);

            //Update navigation properties
            navProp_csUserCUdto_to_csUserDbM(db, itemDto, _item);

            //write to database model
            db.Users.Add(_item);

            //write to database
            db.SaveChanges();
            
            //return the updated item in non-flat mode
            return ReadUser(_item.UserId);   
        }
    }

    //remove user
    public adminInfoDbDto RemoveUser(Guid _id)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Users.RemoveRange(db.Users.Where(u => u.UserId == _id));
            
            //changeTracker info
            int nrRemovedUsers = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csUserDbM) && entry.State == EntityState.Deleted);
            var _info = new adminInfoDbDto();
            _info.nrRemovedUsers = nrRemovedUsers;

            db.SaveChanges();

            return _info;
        }
    }

    //from all id's in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst
    //Error is thrown if no object is found correspodning to an id.
    private static void navProp_csUserCUdto_to_csUserDbM(csMainDbContext db, csUserCUdto _itemDtoSrc, csUserDbM _itemDst)
    {
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
        _itemDst.CommentDbM = _Comments;
    }
}
