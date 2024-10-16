using DbModels;
using Models;
using Models.DTO;

namespace DbRepos;

//seed repo interface
public interface ISeedRepo
{
    public adminInfoDbDto SeedTestdata(int _count);
    public adminInfoDbDto RemoveAllTestdata(bool seeded);
}

//attraction repo interface
public interface IAttractionRepo 
{
    public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city);
    public IAttraction ReadAttraction(Guid _id);
    public List<IAttraction> ReadAttractionsByCity(string _city);
    public List<IAttraction> ReadAttractionsWithoutComments();
    public IAttraction AddAttraction(csAttractionCUdto itemDto);
    public IAttraction UpdateAttraction(csAttractionCUdto itemDto);
    public adminInfoDbDto RemoveAttraction(Guid _id);
}

//user repo interface
public interface IUserRepo 
{
    public List<IUser> ReadUsers();
    public IUser ReadUser(Guid _userId);
    public IUser AddUser(csUserCUdto itemDto);
    public adminInfoDbDto RemoveUser(Guid _id);
}
 //comment repo interface
public interface ICommentRepo 
{
    public List<IComment> ReadComments(Guid _attractionId);
    public IComment ReadComment(Guid _commenId);
    public IComment AddComment(csCommentCUdto itemDto);
    public adminInfoDbDto RemoveComment(Guid _id);
}