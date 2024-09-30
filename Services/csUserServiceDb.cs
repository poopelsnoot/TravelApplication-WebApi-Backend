using Microsoft.Extensions.Logging;
using Models;
using Seido.Utilities.SeedGenerator;

namespace Services;


public class csUserServiceDb : IUserService
{
    public List<IUser> ReadUsers() => throw new NotImplementedException();
    public IUser AddUser() => throw new NotImplementedException();
    public IUser RemoveUser(Guid _id) => throw new NotImplementedException();
}