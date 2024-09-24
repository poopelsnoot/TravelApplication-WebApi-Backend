using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;

using Models;
using Seido.Utilities.SeedGenerator;

namespace DbModels;
public class csAddressDbM : csAddress, ISeed<csAddressDbM>, IEquatable<csAddressDbM>
{
    //primary key
    [Key]
    public override Guid AddressId { get; set; }

    #region implementing IEquatable
    public bool Equals(csAddressDbM other) => (other != null) ?((Street, Zip, City, Country) ==
        (other.Street, other.Zip, other.City, other.Country)) :false;

    public override bool Equals(object obj) => Equals(obj as csAddressDbM);
    public override int GetHashCode() => (Street, Zip, City, Country).GetHashCode();
    #endregion

    #region seed
    public override csAddressDbM Seed(csSeedGenerator _seeder)
    {
        base.Seed(_seeder);
        return this;
    }
    #endregion
    
    #region Implementing IEquatable

    #endregion
}
