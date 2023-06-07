
using Web_Social_network_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.Design;

namespace Web_Social_network_BE.Repositories.LikeRepository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly SocialNetworkN01Ver3Context _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ISession _session;
		public LikeRepository(SocialNetworkN01Ver3Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
			_httpContextAccessor = httpContextAccessor;
			_session = _httpContextAccessor.HttpContext.Session;
		}
        public Task<Like> AddAsync(Like entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Like> AddLikeForComment(long commentId)
        {
            try
            {
                var userId = _session.GetString("UserId");
                var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);
                Like like = new Like();
                like.CommentId = comment.CommentId;
                like.PostId = comment.PostId;
                like.UserId = userId;
                _context.Likes.Add(like);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return like;
            }catch (Exception ex)
            {
                throw new Exception($"Error when like comment {commentId} ", ex);
            }
        }

        public async Task<Like> AddLikeForPost(string postId)
        {
			try
			{
				var userId = _session.GetString("UserId");
				Like like = new Like();
				like.PostId = postId;
				like.UserId = userId;
				_context.Likes.Add(like);
				await _context.SaveChangesAsync().ConfigureAwait(false);
				return like;
            }
            catch (Exception ex)
            {
				throw new Exception($"Error when like Post {postId} ", ex);
			}
		}

        public async Task<IEnumerable<Like>> CheckLikeComment(long commentId)
        {
            try
            {
				var userId = _session.GetString("UserId");
                return await _context.Likes.Where(x => x.CommentId == commentId).Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while check like comment", ex);
            }
        }

        public async Task<IEnumerable<Like>> CheckLikeOfPostAsync(string postId)
        {
			try
			{
				var userId = _session.GetString("UserId");
				return await _context.Likes.Where(x => x.PostId == postId).Where(x => x.UserId == userId).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Error while check like post", ex);
			}
		}

        public async Task DeleteAllForPost(string postId)
        {
            var delete = await _context.Likes.Where(x => x.PostId == postId).ToListAsync(); 
            foreach (var like in delete)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public Task DeleteAsync(long key)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteLikeForComment(long commentId)
        {
			var userId = _session.GetString("UserId");
			var delete = await _context.Likes.Where(x => x.CommentId == commentId).Where(x => x.UserId == userId).FirstOrDefaultAsync();
			if (delete == null)
			{
				throw new Exception("Like is not exits");
			}
			_context.Likes.Remove(delete);
			await _context.SaveChangesAsync().ConfigureAwait(false);
		}

        public async Task DeleteLikeForPost(string postId)
        {
			var userId = _session.GetString("UserId");
			var delete = await _context.Likes.Where(x => x.PostId == postId).Where(x => x.UserId == userId).FirstOrDefaultAsync();
			if (delete == null)
			{
				throw new Exception("Like is not exits");
			}
			_context.Likes.Remove(delete);
			await _context.SaveChangesAsync().ConfigureAwait(false);
		}

        public Task<IEnumerable<Like>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Like>> GetAllLikeOfPost(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<Like> GetByIdAsync(long key)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Like entity)
        {
            throw new NotImplementedException();
        }
    }
}
