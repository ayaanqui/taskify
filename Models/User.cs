using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class User : BaseModel
{
    [Column(TypeName = "varchar(255)")]
    [Required]
    public required string FullName { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string AvatarUrl { get; set; }
}
