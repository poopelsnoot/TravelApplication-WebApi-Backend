using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Models;
using Seido.Utilities.SeedGenerator;
using Models.DTO;

namespace DbModels;

//unique index to not have two identical attractions on the same address
[Index(nameof(AttractionName),nameof(Category), nameof(Description), nameof(AddressId), IsUnique =true)] 
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
    //foreign key property
    [JsonIgnore]
    public virtual Guid AddressId { get; set; }

    #region fixing interface error
    [JsonIgnore]
    [ForeignKey("AddressId")] //foreign key annotation
    public virtual csAddressDbM AddressDbM { get; set; } = null;
    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => new NotImplementedException();}

    [JsonIgnore]
    public virtual List<csCommentDbM> CommentsDbM { get; set; } = null;
    [NotMapped]
    public override List<IComment> Comments { get => CommentsDbM?.ToList<IComment>(); set => new NotImplementedException();}
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

    #region Update from DTO
    public csAttractionDbM UpdateFromDTO(csAttractionCUdto org)
    {
        AttractionName = org.AttractionName;
        Category = org.Category;
        Description = org.Description;

        return this;
    }
    #endregion

    #region constructors
    public csAttractionDbM() { }
    public csAttractionDbM(csAttractionCUdto org)
    {
        AttractionId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion


}