//Owner
using System.ComponentModel.DataAnnotations;
namespace PetClinic1.Models
{
    public class Owner
    {
        [Required]
        public string? Name { get; set; }
        public Contact? Contact { get; set; }
    }
}
