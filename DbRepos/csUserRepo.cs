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
    public List<IUser> ReadUsers() 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var user = db.Users
            .Include(u => u.CommentDbM)
            .ToList<IUser>();
                                                                         
            return user;
        }
    }
    public IUser AddUser() => throw new NotImplementedException();
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
}
