using Microsoft.Extensions.Logging;
using Models;
using Seido.Utilities.SeedGenerator;

namespace Services;


public class csCommentServiceDb : ICommentService
{
    public IComment AddComment() => throw new NotImplementedException();
    public List<IComment> ReadComments(Guid _attractionId) => throw new NotImplementedException();

    public IComment RemoveComment(Guid _id) => throw new NotImplementedException();

}