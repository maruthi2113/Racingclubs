using Runner.Data.Enum;

namespace Runner.Models.ViewModels
{
    public class EditRaceViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }    

        public string Description { get; set; } 

        public IFormFile Image { get; set; }
        public string? URL { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public RaceCategory RaceCategory { get; set; }  
    }
}
