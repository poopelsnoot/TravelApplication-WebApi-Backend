using Configuration;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;
namespace Models;

//attraction interface
public interface IAttraction{
    
    public Guid AttractionId {get; set; }
    public string AttractionName { get; set; }
    public string Category { get; set; }
    public string Description {get; set;}

    //navigation props
    //Attraction can have many comments
    public List<IComment> Comments {get; set;}

    //attraction has one address
    public IAddress Address { get; set; }

}

//address interface
public interface IAddress{
    
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public int Zip {get; set;}
    public string City {get; set;}
    public string Country {get; set;}

    //navigation prop. One address can have many attractions
    public List<IAttraction> Attractions {get; set;}

}

//comment interface
public interface IComment{
    
    public Guid CommentId { get; set; }
    public string Comment { get; set; }

    public DateTime Date {get; set; }

    //navigation props
    //Comment has one user and one attraction
    public IUser User { get; set; }
    public IAttraction Attraction { get; set; }
}

//user interface
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