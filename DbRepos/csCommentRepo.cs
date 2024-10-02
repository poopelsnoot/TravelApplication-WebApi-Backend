using Microsoft.EntityFrameworkCore;

using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;

namespace DbRepos;

public class csCommentRepo : ICommentRepo
{
    
    public IComment AddComment() => throw new NotImplementedException();
    public List<IComment> ReadComments(Guid _attractionId) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var comments = db.Comments.Include(a => a.UserDbM).Where(a => a.AttractionDbM.AttractionId == _attractionId).ToList<IComment>();
                                                                         
            return comments;
        }
    }

    public IComment RemoveComment(Guid _id) => throw new NotImplementedException();

}