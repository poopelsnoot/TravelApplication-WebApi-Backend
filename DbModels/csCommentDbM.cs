using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;

public class csCommentDbM : csComment, ISeed<csCommentDbM>, IEquatable<csCommentDbM>
{
    //primary key
    [Key]
    public override Guid CommentId { get; set; }
    [Required]
    public override string Comment { get; set; }
    [Required]
    public override DateTime Date {get; set; }

    #region fixing interface error
    [JsonIgnore]
    public virtual csAttractionDbM AttractionDbM { get; set; } = null;
    [NotMapped]
    public override IAttraction Attraction { get => AttractionDbM; set => new NotImplementedException();}

    [JsonIgnore]
    public virtual csUserDbM UserDbM { get; set; } = null;
    [NotMapped]
    public override IUser User { get => UserDbM; set => new NotImplementedException();}

    #endregion
    
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

    
}



