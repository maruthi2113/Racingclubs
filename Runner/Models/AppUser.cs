using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Runner.Models
{
    public class AppUser :IdentityUser
    {

        public int? Pace { get; set; }

        public int? Milage {get;set;}
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        

        public ICollection<Club> Clubs { get; set; }

        public ICollection<Race> Races { get; set; }

    }
}
