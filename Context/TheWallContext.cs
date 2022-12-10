#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Context;

public class TheWallContext : DbContext
{
    public TheWallContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<LikePost> LikePosts { get; set; }
    public DbSet<LikeComment> LikeComments { get; set; }
}
