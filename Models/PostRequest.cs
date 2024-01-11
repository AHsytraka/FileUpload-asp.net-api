using System.Text.Json.Serialization;

namespace File_Upload.Models.PostRequest;

public class PostRequest
{
    public int UserId { get; set; }
    public string Description { get; set; }
    public IFormFile File { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? FilePath { get; set; }
}
