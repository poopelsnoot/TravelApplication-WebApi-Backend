using Seido.Utilities.SeedGenerator;
using Models;
using Models.DTO;

namespace Services;

//seed service interface
public interface ISeedService{
    public adminInfoDbDto SeedTestdata(int _count);
    public adminInfoDbDto RemoveAllTestdata(bool seeded);

}

//attraction service interface
public interface IAttractionService{

    public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city);
    public IAttraction ReadAttraction(Guid _id);
    public List<IAttraction> ReadAttractionsByCity(string _city);
    public List<IAttraction> ReadAttractionsWithoutComments();
    public IAttraction AddAttraction(csAttractionCUdto itemDto);
    public IAttraction UpdateAttraction(csAttractionCUdto itemDto);
    public adminInfoDbDto RemoveAttraction(Guid _id); 

}

//user service interface
public interface IUserService{
    public List<IUser> ReadUsers();
    public IUser ReadUser(Guid _cuserId);
    public IUser AddUser(csUserCUdto itemDto);
    public adminInfoDbDto RemoveUser(Guid _id);
}

//comment service interface
public interface ICommentService{
    public List<IComment> ReadComments(Guid _attractionId);
    public IComment ReadComment(Guid _commentId);
    public IComment AddComment(csCommentCUdto itemDto);
    public adminInfoDbDto RemoveComment(Guid _id);

}

