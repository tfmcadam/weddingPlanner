#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace weddingPlannerTwo.Models;


public class Wedding
{
    [Key]
    public int WeddingId { get; set; }

    [Required(ErrorMessage ="Please add Wedder")]
    [Display( Name="Wedder One")]
    public string WedderOne { get;set; }
    

    [Required(ErrorMessage ="Please add Wedder.")]
    [Display( Name="Wedder Two")]
    public string WedderTwo { get;set; }
    

    [Required(ErrorMessage ="Please pick a date.")]
    [DataType(DataType.Date)]
    [FutureDate]
    public DateTime Date { get;set; }
    

    [Required(ErrorMessage ="Your Guests would like to know where to go.")]
    public string Address { get;set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    // navigation property
    // Many to MANY for joining links guests to Users
    public List<Reservation> Guests { get; set; } = new List<Reservation>();
    
    public int UserId {get;set;}

    // One to Many
    public User? User {get;set;}
    

    public bool GoingToWedding(int UserId)
    {
        foreach(Reservation guest in Guests)
        {
            if(guest.UserId == UserId)
            {
                return true;
            }
        }
        return false;
    }
}




