using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models.Generated
{
    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [DefaultValue(null)]
        public string Firstname { get; set; } = null!;

        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? Middlename { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? Lastname { get; set; }

        [Required]
        public int? Age { get; set; }

        [StringLength(1)]
        [Unicode(false)]
        [Required]
        [DefaultValue(null)]
        public string Sex { get; set; } = null!;

        public bool IsEmployed { get; set; }
    }
}