using Configuration;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;

namespace Models;


public class csComment : ISeed<csComment>, IComment, IEquatable<csComment>
{
    
    public virtual Guid CommentId { get; set; } 
    public virtual string Comment { get; set; }
    public virtual DateTime Date {get; set; }
    
    
    //navigation props
    //one comment belongs to one attraction
    public virtual IAttraction Attraction { get; set;}
    // Comment has one user
    public virtual IUser User { get; set; }

    #region implementing IEquatable
    public bool Equals(csComment other) => (other != null) ?((Comment, Date) ==
        (other.Comment, other.Date)) :false;

    public override bool Equals(object obj) => Equals(obj as csComment);
    public override int GetHashCode() => (Comment, Date).GetHashCode();
    #endregion

    #region seeder
    public bool Seeded { get; set; } = false;
    public virtual csComment Seed(csSeedGenerator _seeder)
    {
        Seeded = true;
        CommentId = Guid.NewGuid();
        Comment = _seeder.LatinSentence;
        Date = _seeder.DateAndTime(2022, 2024);
        
        return this;
    }
    #endregion
}



