using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;

[Index(nameof(FirstName),nameof(LastName), nameof(Age), nameof(Email), IsUnique = true)] //unique identifier
public class csUserDbM : csUser, ISeed<csUserDbM>, IEquatable<csUserDbM>
{
    //primary key
    [Key]
    public override Guid UserId { get; set; }
    [Required]
    public override string FirstName { get; set; }
    [Required]
    public override string LastName { get; set; }
    [Required]
    public override int Age { get; set; }
    [Required]
    public override string Email { get; set; }

    #region fixing interface error
    [JsonIgnore]
    public virtual List<csCommentDbM> CommentDbM { get; set; } = null;
    [NotMapped]
    public override List<IComment> Comments { get => CommentDbM?.ToList<IComment>(); set => new NotImplementedException();}
    #endregion
    
    #region implementing IEquatable
    public bool Equals(csUserDbM other) => (other != null) ?((FirstName, LastName, Age, Email) ==
        (other.FirstName, other.LastName, other.Age, other.Email)) :false;

    public override bool Equals(object obj) => Equals(obj as csUserDbM);
    public override int GetHashCode() => (FirstName, LastName, Age, Email).GetHashCode();
    #endregion

    #region seed
    public override csUserDbM Seed(csSeedGenerator _seeder)
    {
        base.Seed(_seeder);
        return this;
    }
    #endregion

}