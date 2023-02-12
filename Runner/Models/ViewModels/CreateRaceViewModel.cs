using MessagePack;
using Runner.Data.Enum;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace Runner.Models.ViewModels
{
    public class CreateRaceViewModel
    {
    
        public int Id { get; set; }
    
        public string Name { get; set; }

        public string Description { get; set; } 

        public IFormFile Image { get; set; }

        public Address Address { get; set; }    

        public RaceCategory RaceCategory { get; set; }

        public string AppUserId { get; set; }
    }
}
