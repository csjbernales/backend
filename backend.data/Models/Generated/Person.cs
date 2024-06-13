using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.data.Models.Generated;

[Table("Person")]
public partial class Person
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

    [StringLength(3)]
    public string Age { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string Sex { get; set; } = null!;

    public bool IsEmployed { get; set; }
}
