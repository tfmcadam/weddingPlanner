using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

using weddingPlannerTwo.Models;
namespace weddingPlannerTwo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Initial View
    public IActionResult Index()
    {
        return View();
    }

    // create a New User
    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (ModelState.IsValid)
        {
            // hash the password prior to saving
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

            // Once Hashed Add the user and Save changes
            _context.Add(newUser);
            _context.SaveChanges();

            // Add Session to begin after user added
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("AllWeddings", "Wedding");
        }
        else
        {
            return View("Index");
        }
    }

    // Success page after info is entered correctly
    [SessionCheck]
    [HttpGet("success")]
    public IActionResult Success(LogUser loginUser)
    {
        User? OneUser = _context.Users.FirstOrDefault();
        return View("AllWeddings", "Wedding");
    }

    // LogInUser
    [HttpPost("users/login")]
    public IActionResult LoginUser(LogUser loginUser)
    {
        if (ModelState.IsValid)
        {
            User? UserInDb = _context.Users.FirstOrDefault(u => u.Email == loginUser.LEmail);

            // verify the user exists
            if (UserInDb == null)
            {
                ModelState.AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(loginUser, UserInDb.Password, loginUser.LPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("UserId", UserInDb.UserId);
            return RedirectToAction("AllWeddings", "Wedding");


        }
        else
        {
            return View("Index");
        }
    }


    // logout time
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }


    // error page
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}






// SessionCheck Attribute
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? UserId = context.HttpContext.Session.GetInt32("UserId");
        if (UserId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}