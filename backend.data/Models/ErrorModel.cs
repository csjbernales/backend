using System.ComponentModel.DataAnnotations.Schema;

namespace backend.data.Models
{
    [NotMapped]
    public class ErrorModel
    {
        public string? ErrorMessage { get; set; }
    }
}