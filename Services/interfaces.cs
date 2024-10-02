using Seido.Utilities.SeedGenerator;

using Models;


namespace Services;
public interface ISeedService{
    public void SeedTestdata();
    public void RemoveAllTestdata();

}

public interface IAttractionService{

    public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city);
    public IAttraction ReadAttraction(Guid _id);
    public List<IAttraction> ReadAttractionsByCity(string _city);
    public List<IAttraction> ReadAttractionsWithoutComments();
    public IAttraction AddAttraction();
    public IAttraction UpdateAttraction(Guid _id);
    public IAttraction RemoveAttraction(Guid _id); 

}

public interface IUserService{
    public List<IUser> ReadUsers(int _count);
    public IUser AddUser();
    public IUser RemoveUser(Guid _id);
}

public interface ICommentService{
    public IComment AddComment();
    public List<IComment> ReadComments(Guid _attractionId);
    public IComment RemoveComment(Guid _id);

}

