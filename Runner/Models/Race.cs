using Runner.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Runner.Models
{
    public class Race
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public RaceCategory RaceCategory { get; set; }

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        
    }
}
