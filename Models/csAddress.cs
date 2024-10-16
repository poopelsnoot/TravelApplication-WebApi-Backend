using Configuration;
using Seido.Utilities.SeedGenerator;

namespace Models;

//address model
public class csAddress : ISeed<csAddress>, IAddress, IEquatable<csAddress>
{
    
    public virtual Guid AddressId { get; set;}
    public virtual string Street { get; set;}
    public virtual int Zip { get; set;}
    public virtual string City { get; set;}
    public virtual string Country { get; set;}

    // navigation props
    // An adress can have many attractions
    public virtual List<IAttraction> Attractions { get; set;}

    #region implementing IEquatable
    public bool Equals(csAddress other) => (other != null) ?((Street, Zip, City, Country) ==
        (other.Street, other.Zip, other.City, other.Country)) :false;

    public override bool Equals(object obj) => Equals(obj as csAddress);
    public override int GetHashCode() => (Street, Zip, City, Country).GetHashCode();
    #endregion

    #region seeder
    public bool Seeded { get; set;} = false;

    public virtual csAddress Seed(csSeedGenerator _seeder)
    {
        Seeded = true;
        AddressId = Guid.NewGuid();
        Country = _seeder.Country;
        Street = _seeder.StreetAddress(Country);
        Zip = -_seeder.ZipCode;
        City = _seeder.City(Country);
        return this;
    }
    #endregion
}