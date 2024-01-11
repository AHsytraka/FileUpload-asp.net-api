namespace File_Upload;

public class PostModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public DateTime Ts { get; set; }
}
