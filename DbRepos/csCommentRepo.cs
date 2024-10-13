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
    public List<IComment> ReadComments(Guid _attractionId) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var comments = db.Comments.Include(a => a.UserDbM).Where(a => a.AttractionDbM.AttractionId == _attractionId).ToList<IComment>();
                                                                         
            return comments;
        }
    }

    public IComment ReadComment(Guid _commentId) 
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            var comment = db.Comments
            .Include(a => a.UserDbM)
            .Include(a => a.AttractionDbM)
            .Where(a => a.CommentId == _commentId).FirstOrDefault();
                                                                         
            return comment;
        }
    }

    public IComment AddComment(csCommentCUdto itemDto)
    {
        using (var db = csMainDbContext.DbContext("sysadmin"))
        {
            throw new NotImplementedException();
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

    //from all id's in _itemDtoSrc finds the corresponding object in the database and assigns it to _itemDst
    //Error is thrown if no object is found correspodning to an id.
    private static void navProp_csCommentCUdto_to_csCommentDbM(csMainDbContext db, csCommentCUdto _itemDtoSrc, csCommentDbM _itemDst)
    {
        //update UserDbM from itemDto.UserId
        _itemDst.UserDbM = (_itemDtoSrc.UserId != null) ? db.Users.FirstOrDefault(
            a => (a.UserId == _itemDtoSrc.UserId)) : null;

        //update AttractionDbM from itemDto.AttractionId
        _itemDst.AttractionDbM = (_itemDtoSrc.AttractionId != null) ? db.Attractions.FirstOrDefault(
            a => (a.AttractionId == _itemDtoSrc.AttractionId)) : null;
    }

}