using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;

namespace DbRepos;

public class csUserRepo : IUserRepo
{
    public List<IUser> ReadUsers(int _count) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var user = db.Users.Include(u => u.CommentDbM).Take(_count).ToList<IUser>();
                                                                         
            return user;
        }
    }
    public IUser AddUser() => throw new NotImplementedException();
    public IUser RemoveUser(Guid _id) => throw new NotImplementedException();
}
