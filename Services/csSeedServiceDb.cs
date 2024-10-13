using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;
using Microsoft.EntityFrameworkCore;
using DbRepos;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;

namespace Services;


public class csSeedServiceDb : ISeedService
{
    private ISeedRepo _repo = null;

    public adminInfoDbDto SeedTestdata(int _count) => _repo.SeedTestdata(_count);
    public adminInfoDbDto RemoveAllTestdata(bool seeded) => _repo.RemoveAllTestdata(true);

    public csSeedServiceDb(ISeedRepo repo)
    {
        _repo = repo;
    }
}