using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;

public class csAttractionDbM : csAttraction, ISeed<csAttractionDbM>, IEquatable<csAttractionDbM>
{
    //primary key
    [Key]
    public override Guid AttractionId { get; set; }
    [Required]
    public override string AttractionName { get; set; }
    [Required]
    public override string Category { get; set; }
    [Required]
    public override string Description { get; set;}

    #region fixing interface error
    [JsonIgnore]
    public virtual csAddressDbM AddressDbM { get; set; } = null;
    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => new NotImplementedException();}

    [JsonIgnore]
    public virtual List<csCommentDbM> CommentDbM { get; set; } = null;
    [NotMapped]
    public override List<IComment> Comments { get => CommentDbM?.ToList<IComment>(); set => new NotImplementedException();}
    #endregion
    
    #region implementing IEquatable
    public bool Equals(csAttractionDbM other) => (other != null) ?((AttractionName, Category, Description) ==
        (other.AttractionName, other.Category, other.Description)) :false;

    public override bool Equals(object obj) => Equals(obj as csAttractionDbM);
    public override int GetHashCode() => (AttractionName, Category, Description).GetHashCode();
    #endregion

    #region seed
    public override csAttractionDbM Seed(csSeedGenerator _seeder)
    {
        base.Seed(_seeder);
        return this;
    }
    #endregion

    #region Implementing IEquatable
    
    #endregion
}