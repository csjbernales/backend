using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.api.Models.Generated;

[Keyless]
[Table("Cart")]
public partial class Cart
{

    public int CustomerId { get; set; }


    public int ProductId { get; set; }
}
