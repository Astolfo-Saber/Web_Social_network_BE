using System.Threading.Tasks;
using Web_Social_network_BE.Models;

namespace Web_Social_network_BE.Repositories.NewfeedRepository
{
	public interface INewfeedRepository : IGeneralRepository<Newfeed, string>
	{
		Task<IEnumerable<Newfeed>> GetAllFavoritePost(string userId);
		Task<Newfeed> AddFavoritePost(string postId, string userId);
		Task DeleteFavoritePost(string postId, string userId);
		Task<Newfeed> HiddenPost(string postId, string userId);
		Task<Newfeed> GetFavoritePost(string postId, string userId);
	}

}
