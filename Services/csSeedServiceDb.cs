using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;
using Microsoft.EntityFrameworkCore;
using DbRepos;
using Microsoft.IdentityModel.Tokens;

namespace Services;


public class csSeedServiceDb : ISeedService
{
    private ISeedRepo _repo = null;

    public void SeedTestdata() => _repo.SeedTestdata();
    public void RemoveAllTestdata() => _repo.RemoveAllTestdata();

    public csSeedServiceDb(ISeedRepo repo)
    {
        _repo = repo;
    }
}