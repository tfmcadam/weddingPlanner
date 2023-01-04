#pragma warning disable CS8618

namespace weddingPlannerTwo.Models;

public class MyWeddingModel
{
    public User? User { get; set; }
    
    public Wedding? Wedding { get; set; }

    public List<Wedding> AllWeddings { get; set; }

    public List<Reservation>? Guests { get; set; }

    public List<Reservation> RSVP { get; set; }

    public Reservation Reservation { get; set; }
}