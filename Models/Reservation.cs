#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;


namespace weddingPlannerTwo.Models;

public class Reservation
{

    [Key]
    public int ReservationId { get; set; }

    public int UserId { get; set; }
    
    public int WeddingId { get; set; }

    public Wedding? Wedding { get; set; }
    public User? User { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}



    
    
