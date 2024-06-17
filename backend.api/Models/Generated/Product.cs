using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace backend.api.Models.Generated;

public partial class Product
{
    /// <summary>
    /// Product ID
    /// </summary>
    [Key]
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Product name
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    [Required]
    [DefaultValue(null)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Product category
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    [Required]
    [DefaultValue(null)]
    public string Category { get; set; } = null!;

    /// <summary>
    /// Product quantity
    /// </summary>
    public int Quantity { get; set; }
}