using Web_Social_network_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web_Social_network_BE.Repositories.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialNetworkN01Ver3Context _context;
        public CommentRepository(SocialNetworkN01Ver3Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetAllCommentByPostIdAsync(string postId)
        {
            try
            {
                return await _context.Comments.Where(u => u.PostId == postId).Where(x=> x.CommentReply == null).OrderByDescending(x=> x.CommentAt).ToListAsync();
            }
            catch
            {
                throw new ArgumentException($"Post with id {postId} does not exist");
            }
        }
		public async Task<IEnumerable<Comment>> GetAllReplyByCommentId(string postId, long commentId)
		{
			try
			{
				return await _context.Comments.Where(u => u.PostId == postId).Where(x=> x.CommentReply == commentId).OrderBy(x => x.CommentAt).ToListAsync();
			}
			catch
			{
				throw new ArgumentException($"Post with id {postId} does not exist");
			}
		}

		public async Task<Comment> AddCommentByPostIdAsync(string postId, Comment entity)
        {
            var postToComment = await _context.Posts.FirstOrDefaultAsync(u => u.PostId == postId).ConfigureAwait(false);
            if (postToComment == null)
            {
                throw new ArgumentException($"Post with id {postId} does not exist");
            }
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }
		public async Task<Comment> AddReplyComment(long commentId, Comment entity)
		{
            var commentToReply = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId).ConfigureAwait(false);
            if (commentToReply == null)
            {
                throw new ArgumentException($"Comment with id {commentId} is not exist");
            }
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
		}


		public async Task<Comment> UpdateReplyComment(long commentId, Comment entity)
		{
            var commentToReply = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId).ConfigureAwait(false);
            if (commentToReply == null)
            {
                throw new ArgumentException($"Comment with id {commentId} is not exist");
            }
            commentToReply.Content = entity.Content;
            commentToReply.CommentAt = DateTime.Now;
            _context.Comments.Update(commentToReply);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
		}


        public async Task DeleteAllCommentsByPostId(string postId)
        {
            try
            {
                var commentOfPost = await _context.Comments.Where(u => u.PostId == postId).Where(x => x.CommentReply == null).ToListAsync();
                foreach(var comment in commentOfPost)
                {
                    var commentToDelete = await _context.Comments.Where(u => u.PostId == postId).Where(x => x.CommentReply == comment.CommentId).ToListAsync();
					foreach (var delete in commentToDelete)
                    {
						_context.Comments.Remove(delete);
						await _context.SaveChangesAsync().ConfigureAwait(false);
					}
					_context.Comments.Remove(comment);
					await _context.SaveChangesAsync().ConfigureAwait(false);
				}

			}
            catch(Exception e)
            {
                throw new Exception($"An error occurred while deleting post's comments with id {postId}.", e);
            }
        }

        public async Task DeleteAsync(long key)
        {
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == key);
                _context.Comments.RemoveRange(comment);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while delete comment {key}", ex);
            } 
        }		
        public async Task<Comment> GetByIdAsync(long key)
		{
			try
			{
				return await _context.Comments.FirstOrDefaultAsync(u => u.CommentId == key);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while getting comment with id {key}.", ex);
			}
		}
		public async Task UpdateAsync(Comment entity)
        {
            try
            {
                var commentToUpdate = await _context.Comments.FindAsync(entity.CommentId);

                if (commentToUpdate == null)
                {
                    throw new ArgumentException($"Comment with id {entity.CommentId} does not exist");
                }

                _context.Comments.Update(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating comment with id {entity.CommentId}.", ex);
            }
        }
        //Khong Dung Den
        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            try
            {
                return await _context.Comments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while get all comment.", ex);
            }
        }

        public Task<Comment> AddAsync(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
