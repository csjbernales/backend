using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.api.Models.Generated;

public partial class Product
{
    [Key]
    [Required]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    [Required]
    [DefaultValue(null)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    [Required]
    [DefaultValue(null)]
    public string Category { get; set; } = null!;

    public int Quantity { get; set; }
}