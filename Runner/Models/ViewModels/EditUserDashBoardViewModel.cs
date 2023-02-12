namespace Runner.Models.ViewModels
{
    public class EditUserDashBoardViewModel
    {
     
      public string Id { get; set; }

      public int? pace { get; set; }
        
      public int? Milage { get; set; }
      
        public string? ImageProfile { get; set; }

        public string? City { get; set; }    
        public string? State { get; set; }
        public IFormFile Image { get; set; }
    }
}
