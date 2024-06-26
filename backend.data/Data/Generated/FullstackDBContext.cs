using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.data.Models.Generated;

namespace backend.data.Data.Generated;

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
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Sex).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
