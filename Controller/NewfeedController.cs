using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Social_network_BE.Repositories.NewfeedRepository;

namespace Web_Social_network_BE.Controller
{
	[Route("v3/api/[controller]")]
	[ApiController]
	public class NewfeedController : ControllerBase
	{
		private readonly INewfeedRepository _newfeedRepository;
		private readonly IHttpContextAccessor _httpcontextAccessor;
		private readonly ISession _session;

		public NewfeedController(INewfeedRepository newfeedRepository, IHttpContextAccessor httpcontextAccessor)
		{
			_newfeedRepository = newfeedRepository;
			_httpcontextAccessor = httpcontextAccessor;
			_session = _httpcontextAccessor.HttpContext.Session;
		}
		[HttpGet("all/{userId}")]
		public async Task<IActionResult> GetAllFavorite(string userId)
		{
			try
			{
				var favorite = await _newfeedRepository.GetAllFavoritePost(userId);
				return Ok(favorite);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
		[HttpGet("favorite/{postId}")]
		public async Task<IActionResult> GetFavorite(string postId)
		{
			var userId = _session.GetString("UserId");
			var favorite = await _newfeedRepository.GetFavoritePost(postId,userId);
			return Ok(favorite);
		}
		[HttpPost("favorite/{postId}")]
		public async Task<IActionResult> AddFavorite(string postId)
		{
			var userId = _session.GetString("UserId");
			var favorite = await _newfeedRepository.AddFavoritePost(postId, userId);
			return Ok(favorite);
		}
		[HttpDelete("favorite/{postId}")]
		public async Task<IActionResult> DeleteFavorite(string postId)
		{
			var userId = _session.GetString("UserId");
			await _newfeedRepository.DeleteFavoritePost(postId, userId);
			return NoContent();
		}
		[HttpPost("hidden/{postId}")]
		public async Task<IActionResult> HiddenPost(string postId)
		{
			var userId = _session.GetString("UserId");
			var hidden = await _newfeedRepository.HiddenPost(postId, userId);
			return Ok(hidden);
		}
	}
}
