﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.api.Models.Generated;

[Table("Customer")]
public partial class Customer
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Firstname { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Middlename { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Lastname { get; set; }

    public int? Age { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Sex { get; set; } = null!;

    public bool IsEmployed { get; set; }
}
