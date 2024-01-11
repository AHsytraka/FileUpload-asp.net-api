using System.ComponentModel.DataAnnotations;

namespace File_Upload.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }
    public string Imagepath { get; set; }

    public DateTime Ts { get; set; }
    // public bool Published { get; set; }
}
