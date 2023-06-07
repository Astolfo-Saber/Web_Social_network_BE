using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;
using Web_Social_network_BE.Models;
using Web_Social_network_BE.Repositories.CommentRepository;
using Web_Social_network_BE.Repositories.UserRepository;

namespace Web_Social_network_BE.Controller
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public CommentController(ICommentRepository commentRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentByPost(string postId)
        {
            try
            {
                var comment = await _commentRepository.GetAllCommentByPostIdAsync(postId);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{postId}/{commentId}")]
        public async Task<IActionResult> GetReplyComment(string postId, long commentId)
        {
			try
			{
				var comment = await _commentRepository.GetAllReplyByCommentId(postId, commentId);
				return Ok(comment);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        [HttpPost("{postId}")]
        public async Task<IActionResult> Add(string postId, [FromBody] Comment comment)
        {
            try
            {
                var userId = _session.GetString("UserId");
                Comment cmt = new Comment();
                cmt.UserId = userId;
                cmt.Content = comment.Content;
                cmt.PostId = postId;
                var newcomment = await _commentRepository.AddCommentByPostIdAsync(postId, cmt);
                return Ok(newcomment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("{postId}/{commentId}")]
        public async Task<IActionResult> AddReply(string postId, long commentId, [FromBody] Comment comment)
        {
			try
			{
				var userId = _session.GetString("UserId");
				Comment cmt = new Comment();
				cmt.UserId = userId;
				cmt.Content = comment.Content;
				cmt.PostId = postId;
                cmt.CommentReply = commentId;
				var newcomment = await _commentRepository.AddCommentByPostIdAsync(postId, cmt);
				return Ok(newcomment);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        [HttpPut("{postId}/{commentId}")]
        public async Task<IActionResult> Update(string postId, long commentId, [FromBody] Comment comment)
        {
			try
			{
				var userId = _session.GetString("UserId");
                Comment edit = new Comment();
                edit.Content = comment.Content;
				var newcomment = await _commentRepository.UpdateReplyComment(commentId, edit);
                return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        [HttpDelete("{postId}/{commentId}")]
        public async Task<IActionResult> Delete(long commentId)
        {
            try
            {
                await _commentRepository.DeleteAsync(commentId);
                return NoContent();
            }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            try
            {
                await _commentRepository.DeleteAllCommentsByPostId(postId);
                return NoContent();
            }
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
    }
}
