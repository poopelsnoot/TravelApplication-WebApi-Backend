using Configuration;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;

namespace Models;

public class csUser : ISeed<csUser>, IUser, IEquatable<csUser>
{

    public virtual Guid UserId {get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual string Email { get; set; }

    //navigation props
    // One user can have many comments
    public virtual List<IComment> Comments { get; set; }

    #region implementing IEquatable
    public bool Equals(csUser other) => (other != null) ?((FirstName, LastName, Age, Email) ==
        (other.FirstName, other.LastName, other.Age, other.Email)) :false;

    public override bool Equals(object obj) => Equals(obj as csUser);
    public override int GetHashCode() => (FirstName, LastName, Age, Email).GetHashCode();
    #endregion

    #region seeder
    public bool Seeded { get; set; } = false;
    public virtual csUser Seed(csSeedGenerator _seeder)
    {
        Seeded = true;
        UserId = Guid.NewGuid();
        FirstName = _seeder.FirstName;
        LastName = _seeder.LastName;
        Age = _seeder.Next(16,90);
        Email = _seeder.Email(FirstName, LastName);
        return this;
    }
    #endregion
}


