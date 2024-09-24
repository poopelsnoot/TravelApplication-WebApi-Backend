using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;
namespace DbModels;

public class csUserDbM : csUser, ISeed<csUserDbM>, IEquatable<csUserDbM>
{
    //primary key
    [Key]
    public override Guid UserId { get; set; }
    
    #region implementing IEquatable
    public bool Equals(csUserDbM other) => (other != null) ?((FirstName, LastName, Age) ==
        (other.FirstName, other.LastName, other.Age)) :false;

    public override bool Equals(object obj) => Equals(obj as csUserDbM);
    public override int GetHashCode() => (FirstName, LastName, Age).GetHashCode();
    #endregion

    #region seed
    public override csUserDbM Seed(csSeedGenerator _seeder)
    {
        base.Seed(_seeder);
        return this;
    }
    #endregion

    #region Implementing IEquatable

    #endregion
}