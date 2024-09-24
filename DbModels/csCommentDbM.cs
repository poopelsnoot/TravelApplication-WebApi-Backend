using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;

public class csCommentDbM : csComment, ISeed<csCommentDbM>, IEquatable<csCommentDbM>
{
    //primary key
    [Key]
    public override Guid CommentId { get; set; }
    
    #region implementing IEquatable
    public bool Equals(csCommentDbM other) => (other != null) ?((Comment, Date) ==
        (other.Comment, other.Date)) :false;

    public override bool Equals(object obj) => Equals(obj as csCommentDbM);
    public override int GetHashCode() => (Comment, Date).GetHashCode();
    #endregion

    #region seed
    public override csCommentDbM Seed(csSeedGenerator _seeder)
    {
        base.Seed(_seeder);
        return this;
    }
    #endregion

    #region Implementing IEquatable

    #endregion
}



