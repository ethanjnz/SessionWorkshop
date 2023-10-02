#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace sessionWorkshop.Models;

public class User
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters!")]
    public string Name { get; set; }
}
