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


public class csUserServiceDb : IUserService
{
    private IUserRepo _repo = null;
    public List<IUser> ReadUsers() => _repo.ReadUsers();
    public IUser AddUser() => _repo.AddUser();
    public adminInfoDbDto RemoveUser(Guid _id) => _repo.RemoveUser(_id);

    public csUserServiceDb(IUserRepo repo)
    {
        _repo = repo;
    }
}