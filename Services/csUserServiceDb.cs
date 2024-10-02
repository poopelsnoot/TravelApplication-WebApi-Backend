using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;
using Microsoft.EntityFrameworkCore;
using DbRepos;

namespace Services;


public class csUserServiceDb : IUserService
{
    private IUserRepo _repo = null;
    public List<IUser> ReadUsers(int _count) => _repo.ReadUsers(_count);
    public IUser AddUser() => _repo.AddUser();
    public IUser RemoveUser(Guid _id) => _repo.RemoveUser(_id);

    public csUserServiceDb(IUserRepo repo)
    {
        _repo = repo;
    }
}