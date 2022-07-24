using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppStore.Model
{
    public class Registration
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "This Field is Mandatory")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Field is Mandatory")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "This Field is Mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Field is Mandatory")]
        public string Password { get; set; }
        public string? Role { get; set; } = "User";
    }
}
