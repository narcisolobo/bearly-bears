using TheWall.Models;
using TheWall.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheWall.Controllers;

public class PostsController : Controller
{
    private TheWallContext _context;

    public PostsController(TheWallContext context)
    {
        _context = context;
    }

    [Authorized]
    [HttpGet("posts")]
    public ViewResult Posts()
    {
        BagTheUserId();
        WallViewModel wallViewModel = new WallViewModel();
        List<Post> allPosts = FetchAllPosts();

        wallViewModel.Posts = allPosts;
        return View(wallViewModel);
    }

    [Authorized]
    [HttpPost("posts")]
    public IActionResult CreatePost(Post post)
    {
        if (ModelState.IsValid)
        {
            _context.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Posts");
        }

        foreach (KeyValuePair<string, ModelStateEntry> error in ModelState)
        {
            Console.WriteLine("\n");
            Console.WriteLine("********** ERROR ********");
            Console.WriteLine($"Field: {error.Key}");
            foreach (ModelError err in error.Value.Errors)
            {
                Console.WriteLine($"Error: {err.ErrorMessage}");
            }
        }

        WallViewModel wallViewModel = new WallViewModel();
        List<Post> allPosts = FetchAllPosts();

        wallViewModel.Posts = allPosts;
        return View("Posts", wallViewModel);
    }

    [Authorized]
    [HttpPost("comments/{postId}")]
    public IActionResult CreateComment(int postId, Comment comment)
    {
        comment.PostId = postId;
        Console.WriteLine(comment);
        if (ModelState.IsValid)
        {
            _context.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Posts");
        }

        WallViewModel wallViewModel = new WallViewModel();
        List<Post> allPosts = FetchAllPosts();

        wallViewModel.Posts = allPosts;
        return View("Posts", wallViewModel);
    }

    public List<Post> FetchAllPosts()
    {
        List<Post> allPosts = _context.Posts
            .Include(m => m.User)
            .Include(m => m.Comments)
            .ThenInclude(c => c.User)
            .ToList();

        return allPosts;
    }

    public void BagTheUserId()
    {
        int userId = 0;
        int? sessionUserId = HttpContext.Session.GetInt32("userId");
        if (sessionUserId is not null)
        {
            userId = (int)sessionUserId;
        }
        ViewBag.userId = userId;
    }
}

public class AuthorizedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? _userId = context.HttpContext.Session.GetInt32("userId");
        if (_userId is null)
        {
            context.Result = new RedirectToActionResult("LoginRegister", "Users", null);
        }
    }
}
