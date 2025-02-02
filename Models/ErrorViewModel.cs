using System.ComponentModel.DataAnnotations;

namespace SelfServiceStudentSystem.Models
{
    public class ErrorViewModel
    {
        [Display(Name = "Request ID")]
        public string? RequestId { get; init; } // "init" e bën të pandryshueshme pas inicializimit

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
