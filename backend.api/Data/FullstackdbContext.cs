using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.api.Models.Generated;

namespace backend.api.Data;

public partial class FullstackDBContext : DbContext
{
    public FullstackDBContext()
    {
    }

    public FullstackDBContext(DbContextOptions<FullstackDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=PREDATOHELIOS16;Database=FullstackDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

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
