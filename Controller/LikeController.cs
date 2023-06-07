using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Social_network_BE.Models;
using Web_Social_network_BE.Repositories.LikeRepository;
using Web_Social_network_BE.Repositories.UserRepository;

namespace Web_Social_network_BE.Controller
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public LikeController(ILikeRepository likeRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> CheckLikePost(string postId)
        {
            try
            {
                var like = await _likeRepository.CheckLikeOfPostAsync(postId);
                return Ok(like);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("comment/{commentId}")]
        public async Task<IActionResult> CheckLikeComment(long commentId)
        {
			try
			{
				var like = await _likeRepository.CheckLikeComment(commentId);
				return Ok(like);
			}
			catch
			{
				return StatusCode(500);
			}
		}
        [HttpPost("{postId}")]
        public async Task<IActionResult> AddLikePost(string postId)
        {
            try
            {
                var like = await _likeRepository.AddLikeForPost(postId);
                return Ok(like);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("comment/{commentId}")]
		public async Task<IActionResult> AddLikeComment(long commentId)
		{
			try
			{
				var like = await _likeRepository.AddLikeForComment(commentId);
				return Ok(like);
			}
			catch
			{
				return BadRequest();
			}
		}
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeleteLikePost(string postId)
        {
            try
            {
                await _likeRepository.DeleteLikeForPost(postId);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
		[HttpDelete("comment/{commentId}")]
		public async Task<IActionResult> DeleteLikeComment(long commentId)
		{
			try
			{
				await _likeRepository.DeleteLikeForComment(commentId);
				return NoContent();
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpDelete("post/{postId}")]
		public async Task<IActionResult> DeleteAllLikePost(string postId)
		{
			try
			{
				await _likeRepository.DeleteAllForPost(postId);
				return NoContent();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}
