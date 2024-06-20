using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models
{
    [NotMapped]
    public class ErrorModel
    {
        public string? ErrorMessage { get; set; }
    }
}