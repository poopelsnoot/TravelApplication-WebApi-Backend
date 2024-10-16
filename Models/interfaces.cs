using Configuration;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;
namespace Models;

public interface IAttraction{
    
    public Guid AttractionId {get; set; }
    public string AttractionName { get; set; }
    public string Category { get; set; }
    public string Description {get; set;}

    //navigation props
    // Attractions can have several comments
    public List<IComment> Comments {get; set;}

    //An attraction will have one address
    public IAddress Address { get; set; }

}

public interface IAddress{
    
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public int Zip {get; set;}
    public string City {get; set;}
    public string Country {get; set;}

    //navigation props. One address can have many attractions
    public List<IAttraction> Attractions {get; set;}

}

public interface IComment{
    
    public Guid CommentId { get; set; }
    public string Comment { get; set; }

    public DateTime Date {get; set; }

    //navigation props
    // Comment has one user and one attraction
    public IUser User { get; set; }
    public IAttraction Attraction { get; set; }
}


public interface IUser{
    public Guid UserId {get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    //Navigation props
    // One user can have many comments
    public List<IComment> Comments { get; set; }
}