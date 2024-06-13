using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.data.Models.Generated;

namespace backend.data.Data;

public partial class FullstackdbContext : DbContext
{
    public FullstackdbContext()
    {
    }

    public FullstackdbContext(DbContextOptions<FullstackdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:fullstackdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.Age).IsFixedLength();
            entity.Property(e => e.Sex).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
