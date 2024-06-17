using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models.Generated
{
    [Table("Customer")]
    public partial class Customer
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Customer first name
        /// </summary>
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [DefaultValue(null)]
        public string Firstname { get; set; } = null!;

        /// <summary>
        /// Customer middlename
        /// </summary>
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? Middlename { get; set; }

        /// <summary>
        /// Customer last name
        /// </summary>
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? Lastname { get; set; }

        /// <summary>
        /// Custoemr age
        /// </summary>
        [Required]
        public int? Age { get; set; }

        /// <summary>
        /// Customer gender (M/F)
        /// </summary>
        [StringLength(1)]
        [Unicode(false)]
        [Required]
        [DefaultValue(null)]
        public string Sex { get; set; } = null!;

        /// <summary>
        /// is customer employed
        /// </summary>
        public bool IsEmployed { get; set; }
    }
}