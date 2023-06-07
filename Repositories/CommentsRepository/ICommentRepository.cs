using Web_Social_network_BE.Models;
using Web_Social_network_BE.Repositories;

namespace Web_Social_network_BE.Repositories.CommentRepository
{
    public interface ICommentRepository : IGeneralRepository<Comment, long>
    {

        Task<IEnumerable<Comment>> GetAllCommentByPostIdAsync(string postId);
        Task<IEnumerable<Comment>> GetAllReplyByCommentId(string postId,long commentId);
        Task<Comment> AddCommentByPostIdAsync(string postId, Comment entity);
        Task<Comment> AddReplyComment(long commentId, Comment entity);
        Task<Comment> UpdateReplyComment(long commentId, Comment entity);
        Task DeleteAllCommentsByPostId(string postId);

    }
}
