using Microsoft.AspNetCore.Mvc;
using weddingPlannerTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace weddingPlannerTwo.Controllers;

public class WeddingController : Controller
{
    private readonly ILogger<WeddingController> _logger;

    private MyContext _context;

    public WeddingController(ILogger<WeddingController>logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    // View All Weddings
    [SessionCheck]
    [HttpGet("weddings")]
    public IActionResult AllWeddings()
    {
        // ViewBag.LogUser = _context.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.
        // GetInt32("UserId"));
        MyWeddingModel MyModel = new MyWeddingModel
        {
            AllWeddings =  _context.Weddings.Include(all => all.Guests).ToList(),
            User = _context.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId")),
            
        };
        return View("AllWeddings", MyModel);
    }

    // New Wedding
    [SessionCheck]
    [HttpGet("weddings/new")]
    public IActionResult AddAWedding()
    {
        ViewBag.LogUser = _context.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
        return View("AddAWedding");
    }

    // Create a Wedding
    [SessionCheck]
    [HttpPost("weddings/create")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newWedding);
            _context.SaveChanges();
            return RedirectToAction("OneWedding");
        }
        return AddAWedding();
    }

    // OneWedding
    [SessionCheck]
    [HttpGet("weddings/{weddingId}")]
    public IActionResult OneWedding(int WeddingId)
    {
        MyWeddingModel OneWedding = new MyWeddingModel
        {
            Wedding = _context.Weddings
                        .Include(wed => wed.Guests)
                        .ThenInclude(user => user.User)
                        .FirstOrDefault(wed => wed.WeddingId == WeddingId),
            User = _context.Users.FirstOrDefault(user =>user.UserId == HttpContext.Session.GetInt32("UserId"))
        };
        return View("OneWedding", OneWedding);
    }

    // Delete a wedding
    [SessionCheck]
    [HttpPost("weddings/{weddingId}/destroy")]
    public IActionResult DestroyWedding(int weddingId)
    {
        Wedding? WeddingToDestroy = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == weddingId);
        _context.Remove(WeddingToDestroy);
        _context.SaveChanges();
        return AllWeddings();
    }

    // RSVP
    [SessionCheck]
    [HttpPost("reservations/rsvp")]
    public IActionResult RSVPWedding(Reservation newRSVP)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newRSVP);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");
        }
        return View("AllWeddings");
    }

    // UnRSVP
    [SessionCheck]
    [HttpPost("reservations/{weddingId}/destroy")]
    public IActionResult UnRSVPWedding(int weddingId)
    {
        Reservation? ReservationToDestroy = _context.Reservations
                                                    .Where(res => res.UserId == HttpContext.Session.GetInt32("UserId"))
                                                    .SingleOrDefault(wed => wed.WeddingId == weddingId);
        _context.Reservations.Remove(ReservationToDestroy);
        _context.SaveChanges();
        return RedirectToAction("AllWeddings");
    }

}