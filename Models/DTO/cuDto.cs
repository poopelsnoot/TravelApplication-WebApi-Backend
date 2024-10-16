using System;
namespace Models.DTO
{
    //user DTO
    public class csUserCUdto 
    {
        public virtual Guid? UserId {get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual string Email { get; set; }
        public virtual List<Guid> CommentsId { get; set; } = null;

        public csUserCUdto(){}
        public csUserCUdto (IUser org) {
            UserId = org.UserId;
            FirstName = org.FirstName;
            LastName = org.LastName;
            Age = org.Age;
            Email = org.Email;

            CommentsId = org.Comments?.Select(c => c.CommentId).ToList();
        }
    }

    //comment DTO
    public class csCommentCUdto
    {
        public virtual Guid? CommentId { get; set; } 
        public virtual string Comment { get; set; }
        public virtual DateTime Date {get; set; } 

        public virtual Guid? UserId { get; set; } = null;
        public virtual Guid? AttractionId { get; set;} = null;

        public csCommentCUdto(){}
        public csCommentCUdto(IComment org)
        {
            CommentId = org.CommentId;
            Comment = org.Comment;
            Date = org.Date;

            AttractionId = org?.Attraction?.AttractionId;
            UserId = org?.User?.UserId;
        }
    }

    //attraction DTO
    public class csAttractionCUdto
    {
        public virtual Guid? AttractionId {get; set; } 
        public virtual string AttractionName { get; set; }
        public virtual string Category { get; set; }
        public virtual string Description { get; set;}

        public virtual Guid? AddressId { get; set;} = null;
        public virtual List<Guid> CommentsId {get; set;} = null;

        public csAttractionCUdto(){}
        public csAttractionCUdto(IAttraction org)
        {
            AttractionId = org.AttractionId;
            AttractionName = org.AttractionName;
            Category = org.Category;
            Description = org.Description;

            CommentsId = org.Comments?.Select(c => c.CommentId).ToList();
            AddressId = org?.Address?.AddressId;
        }
    }
}
