using Web_Social_network_BE.Models;
using Web_Social_network_BE.Repositories;

namespace Web_Social_network_BE.Repositories.LikeRepository
{
    public interface ILikeRepository : IGeneralRepository<Like, long>
    {
        Task<IEnumerable<Like>> GetAllLikeOfPost(string postId);
        Task<IEnumerable<Like>> CheckLikeOfPostAsync(string postId);
        Task<IEnumerable<Like>> CheckLikeComment(long commentId);
        Task<Like> AddLikeForPost(string postId);
        Task<Like> AddLikeForComment(long commentId);
        Task DeleteLikeForPost(string postId);
        Task DeleteLikeForComment(long commentId);
        Task DeleteAllForPost(string postId);
    }
}
