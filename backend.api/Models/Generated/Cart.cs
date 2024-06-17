using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models.Generated;

/// <summary>
/// Cart
/// </summary>
[Keyless]
[Table("Cart")]
public partial class Cart
{

    /// <summary>
    /// CustomerId
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// ProductId
    /// </summary>

    public int ProductId { get; set; }
}
