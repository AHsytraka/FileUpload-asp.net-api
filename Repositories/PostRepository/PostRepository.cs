using File_Upload.Data;
using File_Upload.Helpers;
using File_Upload.Models;
using File_Upload.Models.PostRequest;
using File_Upload.Models.Response;

namespace File_Upload.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SocialDbContext _socialDbContext;
    private readonly IWebHostEnvironment _environment;

    public PostRepository(SocialDbContext socialDbContext, IWebHostEnvironment environment)
    {
        _socialDbContext = socialDbContext;
        _environment = environment;
    }

    public async Task<PostResponse> CreatePostAsync(PostRequest postRequest)
    {
        var post = new Post
        {
            UserId = postRequest.UserId,
            Description = postRequest.Description,
            Imagepath = postRequest.FilePath,
            Ts = DateTime.Now,
            // Published = true
        };

        var postEntry = await _socialDbContext.Post.AddAsync(post);

        var saveResponse = await _socialDbContext.SaveChangesAsync();

        if (saveResponse < 0)
        {
            return new PostResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
        }

        var postEntity = postEntry.Entity;

        var postModel = new PostModel
        {
            Id = postEntity.Id,
            Description = postEntity.Description,
            Ts = postEntity.Ts,
            FilePath = Path.Combine(postEntity.Imagepath),
            UserId = postEntity.UserId
        };


        return new PostResponse { Success = true, Post = postModel };

    }

    public async Task SavePostImageAsync(PostRequest postRequest)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(postRequest.File.FileName);

        if (_environment.WebRootPath == null)
        {
            throw new ArgumentNullException(nameof(_environment.WebRootPath));
        }

        if (uniqueFileName == null)
        {
            throw new ArgumentNullException(nameof(uniqueFileName));
        }

        var uploads = Path.Combine(_environment.WebRootPath, "users", "posts", postRequest.UserId.ToString());

        var filePath = Path.Combine(uploads, uniqueFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        await postRequest.File.CopyToAsync(new FileStream(filePath, FileMode.Create));

        postRequest.FilePath = filePath;

        return;
    }
}
