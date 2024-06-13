using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.api.Models
{
    [NotMapped]
    public class ErrorModel
    {
        public string? ErrorMessage { get; set; }
    }
}
