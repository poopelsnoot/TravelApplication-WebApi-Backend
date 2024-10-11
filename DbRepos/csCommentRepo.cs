using Microsoft.EntityFrameworkCore;
using Configuration;
using Models;
using DbModels;
using DbContext;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

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

    public adminInfoDbDto RemoveComment(Guid _id) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            db.Comments.RemoveRange(db.Comments.Where(c => c.CommentId == _id));
            
            //changeTracker info
            int nrRemovedComments = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csCommentDbM) && entry.State == EntityState.Deleted);
            var _info = new adminInfoDbDto();
            _info.nrRemovedComments = nrRemovedComments;

            db.SaveChanges();

            return _info;
        }
    }

}