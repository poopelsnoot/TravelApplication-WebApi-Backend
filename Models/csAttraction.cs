using Configuration;
using System.ComponentModel.DataAnnotations;
using Seido.Utilities.SeedGenerator;
namespace Models;

//attraction model
public class csAttraction: ISeed<csAttraction>, IAttraction, IEquatable<csAttraction>
{
    public virtual Guid AttractionId {get; set; } 
    public virtual string AttractionName { get; set; }
    public virtual string Category { get; set; }
    public virtual string Description { get; set;}


    // navigation props
    // Attraction can have many comments
    public virtual List<IComment> Comments {get; set;}

    // attraction have one address
    public virtual IAddress Address { get; set;}

    #region implementing IEquatable
    public bool Equals(csAttraction other) => (other != null) ?((AttractionName, Category, Description) ==
        (other.AttractionName, other.Category, other.Description)) :false;

    public override bool Equals(object obj) => Equals(obj as csAttraction);
    public override int GetHashCode() => (AttractionName, Category, Description).GetHashCode();
    #endregion

    #region seeder
    public bool Seeded { get; set;} = false;
    public virtual csAttraction Seed(csSeedGenerator _seeder)
    {
        Seeded = true;
        AttractionId = Guid.NewGuid();
        AttractionName = _seeder.FromString("Eifel Tower, The pentagon, Statue of Liberty, Wasa Museum, Big Ben, Sydney Opera House, Great Wall of China, Louvre Museum, Colosseum, Taj Mahal, Burj Khalifa, Machu Picchu, Christ the Redeemer, Petra, Blue Lagoon, Big Ben");
        Category = _seeder.FromString("Restaurant, Museum, Hike, Beach, Park, Gallery, Monument, Theater, Aquarium, Zoo, Cafe, Lake, Architecture, Culture, Statue");
        Description = _seeder.LatinSentence;
        
        return this;
    }
    #endregion
}

