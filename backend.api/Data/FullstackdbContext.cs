using backend.api.Models.Generated;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //todo: fix this; follow guide
    {
        optionsBuilder.UseSqlServer("Data Source=PREDATOHELIOS16;Database=fullstackdb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

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