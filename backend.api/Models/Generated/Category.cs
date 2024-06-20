using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.api.Models.Generated;

[Table("Category")]
public partial class Category
{
    [Key]
    public int Id { get; set; }
}
