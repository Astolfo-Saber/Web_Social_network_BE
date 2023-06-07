using Microsoft.EntityFrameworkCore;
using Web_Social_network_BE.Models;

namespace Web_Social_network_BE.Repositories.NewfeedRepository
{

	public class NewfeedRepository : INewfeedRepository
	{
		private readonly SocialNetworkN01Ver3Context _context;
		public NewfeedRepository(SocialNetworkN01Ver3Context context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Newfeed>> GetAllFavoritePost(string userId)
		{
			try
			{
				return await _context.Newfeeds.Where(x => x.UserId == userId).Where(x => x.Type == "FAVORITE").ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception("Error while get all favorite post of userId ", ex);
			}
		}

		public async Task<Newfeed> AddFavoritePost(string postId, string userId)
		{
			Newfeed newfeed = new Newfeed();
			newfeed.UserId = userId;
			newfeed.PostId = postId;
			newfeed.Type = "FAVORITE";
			_context.Newfeeds.Add(newfeed);
			await _context.SaveChangesAsync().ConfigureAwait(false);
			return newfeed;
		}
		public async Task DeleteFavoritePost(string postId, string userId)
		{
			var post = await _context.Newfeeds.Where(x => x.UserId == userId).Where(x => x.PostId == postId).FirstOrDefaultAsync();
			_context.Newfeeds.Remove(post);
			await _context.SaveChangesAsync().ConfigureAwait(false);
		}	

		public async Task<Newfeed> HiddenPost(string postId, string userId)
		{
			Newfeed newfeed = new Newfeed();
			newfeed.UserId = userId;
			newfeed.PostId = postId;
			newfeed.Type = "HIDDEN";
			_context.Newfeeds.Add(newfeed);
			await _context.SaveChangesAsync().ConfigureAwait(false);
			return newfeed;
		}
		public async Task<Newfeed> GetFavoritePost(string postId, string userId)
		{
			return await _context.Newfeeds.Where(x => x.UserId == userId).Where(x => x.PostId == postId).FirstOrDefaultAsync();
		}

		public Task<Newfeed> AddAsync(Newfeed entity)
		{
			throw new NotImplementedException();
		}
		public Task DeleteAsync(string key)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<Newfeed>> GetAllAsync()
		{
			throw new NotImplementedException();
		}
		public Task<Newfeed> GetByIdAsync(string key)
		{
			throw new NotImplementedException();
		}
		public Task UpdateAsync(Newfeed entity)
		{
			throw new NotImplementedException();
		}


	}
}
