using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;
using Microsoft.EntityFrameworkCore;
using DbRepos;
using Models.DTO;

namespace Services;


public class csAttractionServiceDb : IAttractionService
{
    private IAttractionRepo _repo = null;
    public List<IAttraction> ReadAttractions(int _count, string _category, string _attractionName, string _description, string _country, string _city) 
    => _repo.ReadAttractions(_count, _category, _attractionName, _description, _country, _city);
    public IAttraction ReadAttraction(Guid _id) => _repo.ReadAttraction(_id);
    public List<IAttraction> ReadAttractionsByCity(string _city) => _repo.ReadAttractionsByCity(_city);
    public List<IAttraction> ReadAttractionsWithoutComments() => _repo.ReadAttractionsWithoutComments();
    public IAttraction AddAttraction(csAttractionCUdto itemDto) => _repo.AddAttraction(itemDto);
    public IAttraction UpdateAttraction(csAttractionCUdto itemDto) => _repo.UpdateAttraction(itemDto);
    public adminInfoDbDto RemoveAttraction(Guid _id) => _repo?.RemoveAttraction(_id);

    public csAttractionServiceDb(IAttractionRepo repo)
    {
        _repo = repo;
    }

}