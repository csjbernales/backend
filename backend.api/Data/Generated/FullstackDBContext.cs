using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.api.Models.Generated;

namespace backend.api.Data.Generated;

public partial class FullstackDBContext : DbContext
{
    public FullstackDBContext(DbContextOptions<FullstackDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Person");

            entity.Property(e => e.Sex).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
