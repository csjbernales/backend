using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.api.Models.Generated;

[Keyless]
[Table("Cart")]
public partial class Cart
{
    public int CustomerId { get; set; }

    public int ProductId { get; set; }
}
