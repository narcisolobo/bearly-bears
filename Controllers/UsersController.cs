using TheWall.Models;
using TheWall.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace TheWall.Controllers;

public class UsersController : Controller
{
    private TheWallContext _context;

    public UsersController(TheWallContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public ViewResult LoginRegister()
    {
        return View();
    }

    [HttpPost("users/register")]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", user.UserId);
            HttpContext.Session.SetString("username", $"{user.FirstName} {user.LastName}");
            return RedirectToAction("Posts", "Posts");
        }
        return View("LoginRegister");
    }

    [HttpPost("users/login")]
    public IActionResult Login(LoginUser loginUser)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);
        if (user is null)
        {
            ModelState.AddModelError("LogEmail", "Email not found.");
            return View("LoginRegister");
        }

        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(loginUser, user.Password, loginUser.LogPassword);
        if (result == 0)
        {
            ModelState.AddModelError("LogPassword", "Incorrect Password.");
            return View("LoginRegister");
        }

        HttpContext.Session.SetInt32("userId", user.UserId);
        HttpContext.Session.SetString("username", $"{user.FirstName} {user.LastName}");
        return RedirectToAction("Posts", "Posts");
    }

    [HttpPost("users/logout")]
    public RedirectToActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginRegister");
    }
}
