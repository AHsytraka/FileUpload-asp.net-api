using File_Upload.Models.PostRequest;
using File_Upload.Models.Response;

namespace File_Upload.Repositories;

public interface IPostRepository
{
    Task SavePostImageAsync(PostRequest postRequest);
    Task<PostResponse> CreatePostAsync(PostRequest postRequest);

}
