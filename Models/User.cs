#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace weddingPlannerTwo.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please Enter a First name.")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 Characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please Enter a Last name.")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 Characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please Enter an Email.")]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please Enter a Password.")]
    [MinLength(8, ErrorMessage = "Password must be atleast 8 Charactors")]
    public string Password { get; set; }

    // Always add created and updated at
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // navigation property
    public List<Reservation> RSVP { get; set; } = new List<Reservation>();
    public List<Wedding> PlannedWeddings { get; set; } = new List<Wedding>();


    // Confirm password is a NotMapped so it is not saved to database
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "Passwords must match.")]
    public string Confirm { get; set; }
}