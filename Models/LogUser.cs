#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace weddingPlannerTwo.Models;

public class LogUser
{

    [Required(ErrorMessage = "Email/Password is Required")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string LEmail { get; set; }

    
    
    [Required(ErrorMessage = "Email/Password is required")]
    [MinLength(8, ErrorMessage = "Must be atleast 8 Charactors")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string LPassword { get; set; }
}
    


