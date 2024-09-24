using DbModels;
namespace DbRepos;

public interface IAttractionRepo
{
    public Task<List<csAttractionDbM>> ListAttractions(int _count);
    public Task Seed(int _count);
}