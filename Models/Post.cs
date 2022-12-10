#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Post
{
    [Key]
    public int PostId { get; set; }

    [Required(ErrorMessage = "Post is empty.")]
    public string Content { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<LikePost> Likes { get; set; } = new List<LikePost>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"UserId: {UserId}, PostId: {PostId}, Content: {Content}";
    }
}
