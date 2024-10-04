using DbModels;
using Models;
using Models.DTO;
namespace DbRepos;

public interface ISeedRepo
{
    public Task<adminInfoDbDto> SeedTestdata();
    public Task<adminInfoDbDto> RemoveAllTestdata(bool seeded);
}

public interface IAttractionRepo 
{
    public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city);
    public IAttraction ReadAttraction(Guid _id);
    public List<IAttraction> ReadAttractionsByCity(string _city);
    public List<IAttraction> ReadAttractionsWithoutComments();
    public IAttraction AddAttraction();
    public IAttraction UpdateAttraction(Guid _id);
    public IAttraction RemoveAttraction(Guid _id);
}

public interface IUserRepo 
{
    public List<IUser> ReadUsers(int _count);
    public IUser AddUser();
    public IUser RemoveUser(Guid _id);
}

public interface ICommentRepo 
{
    public IComment AddComment();
    public List<IComment> ReadComments(Guid _attractionId);
    public IComment RemoveComment(Guid _id);
}