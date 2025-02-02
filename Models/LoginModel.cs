using System.ComponentModel.DataAnnotations;

namespace SelfServiceStudentSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email është i detyrueshëm")]
        [EmailAddress(ErrorMessage = "Adresa e emailit nuk është e vlefshme")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fjalëkalimi është i detyrueshëm")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
