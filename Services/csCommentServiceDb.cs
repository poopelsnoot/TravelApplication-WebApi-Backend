using Microsoft.Extensions.Logging;
using Models;
using DbModels;
using Seido.Utilities.SeedGenerator;
using Configuration;
using DbContext;
using Microsoft.EntityFrameworkCore;
using DbRepos;

namespace Services;


public class csCommentServiceDb : ICommentService
{
    private ICommentRepo _repo = null;
    public IComment AddComment() => _repo.AddComment();
    public List<IComment> ReadComments(Guid _attractionId) => _repo.ReadComments(_attractionId);
    public IComment RemoveComment(Guid _id) => _repo.RemoveComment(_id);

    public csCommentServiceDb(ICommentRepo repo)
    {
        _repo = repo;
    }

}