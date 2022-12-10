#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Required(ErrorMessage = "Comment is empty.")]
    public string CommentContent { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int PostId { get; set; }
    public Post? Post { get; set; }

    public List<LikeComment> Likes { get; set; } = new List<LikeComment>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"UserId: {UserId}, PostId: {PostId}, CommentId: {CommentId}, Content: {CommentContent}";
    }
}
