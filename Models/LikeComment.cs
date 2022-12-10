#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TheWall.Models;

public class LikeComment
{
    [Key]
    public int LikeCommentId { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int CommentId { get; set; }
    public Comment? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"UserId: {UserId}, CommentId: {CommentId}";
    }
}
