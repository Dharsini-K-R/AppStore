using System.ComponentModel.DataAnnotations;

namespace AppStore.Model
{
    public class AppModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AppName { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public int? Ratings { get; set; }
        public int? No_of_Downloads { get; set; }
        
    }
}
