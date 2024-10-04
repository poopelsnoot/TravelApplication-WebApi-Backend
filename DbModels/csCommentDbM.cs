using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;
[Index(nameof(Comment), nameof(Date))] //index without isunique because technically people can write identical comments at the same time 
public class csCommentDbM : csComment, ISeed<csCommentDbM>, IEquatable<csCommentDbM>
{
    //primary key
    [Key]
    public override Guid CommentId { get; set; }
    [Required]
    public override string Comment { get; set; }
    [Required]
    public override DateTime Date {get; set; }

    //foreign key property
    [JsonIgnore]
    public virtual Guid UserId { get; set; }

    //foreign key property
    [JsonIgnore]
    public virtual Guid AttractionId { get; set; }

    #region fixing interface error
    [JsonIgnore]
    [ForeignKey("AttractionId")] //foreign key annotation
    public virtual csAttractionDbM AttractionDbM { get; set; } = null;
    [NotMapped]
    public override IAttraction Attraction { get => AttractionDbM; set => new NotImplementedException();}

    [JsonIgnore]
    [ForeignKey("UserId")] //foreign key annotation
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



