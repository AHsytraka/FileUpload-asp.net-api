using File_Upload.Models;
using Microsoft.EntityFrameworkCore;

namespace File_Upload.Data;

public partial class SocialDbContext : DbContext
{
    public SocialDbContext()

    {

    }

    public SocialDbContext(DbContextOptions<SocialDbContext> options): base(options)
    {

    }

    public virtual DbSet<Post> Post { get; set; }
}
