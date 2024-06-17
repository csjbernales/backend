using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models
{
    /// <summary>
    /// ErrorModel
    /// </summary>
    [NotMapped]
    public class ErrorModel
    {
        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
