using File_Upload.Models.PostRequest;
using File_Upload.Models.Response;
using File_Upload.Repositories;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace File_Upload.Controllers;

[ApiController]
[Route("[Controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _postRepository;

    public PostController(ILogger<PostController> logger, IPostRepository postRepository)
    {
        _logger = logger;
        _postRepository = postRepository;
    }

    [HttpPost("CreatePost")]
    [RequestSizeLimit(5 * 1024 * 1024)] //Max file size allowed to be posted = 5MB
    public async Task<IActionResult> CreatePost([FromForm] PostRequest postRequest)
    {
        if (postRequest == null)
        {
            return BadRequest(new PostResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });
        }

        if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))
        {
            return BadRequest(new PostResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
        }


        if (postRequest.File != null)
        {
            await _postRepository.SavePostImageAsync(postRequest);
        }

        var postResponse = await _postRepository.CreatePostAsync(postRequest);

        if (!postResponse.Success)

        {
            return NotFound(postResponse);
        }

        return Ok(postResponse.Post);
    }
}
