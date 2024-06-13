using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.api.Models.Generated;

namespace backend.api.Data;

public partial class FullstackdbContext : DbContext
{
    public FullstackdbContext()
    {
    }

    public FullstackdbContext(DbContextOptions<FullstackdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:fullstackdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Person");

            entity.Property(e => e.Age).IsFixedLength();
            entity.Property(e => e.Sex).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
