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


public class csCommentServiceDb : ICommentService
{
    private ICommentRepo _repo = null;
    public List<IComment> ReadComments(Guid _attractionId) => _repo.ReadComments(_attractionId);
    public IComment ReadComment(Guid _commentId) => _repo.ReadComment(_commentId);
    public IComment AddComment(csCommentCUdto itemDto) => _repo.AddComment(itemDto);
    public adminInfoDbDto RemoveComment(Guid _id) => _repo.RemoveComment(_id);


    public csCommentServiceDb(ICommentRepo repo)
    {
        _repo = repo;
    }

}