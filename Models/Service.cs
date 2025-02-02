using System.ComponentModel.DataAnnotations;

namespace SelfServiceStudentSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Lloji i shërbimit nuk mund të jetë më shumë se 255 karaktere.")]
        public string ServiceType { get; set; } = "";

        [MaxLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere.")]
        public string? Description { get; set; }
    }
}
