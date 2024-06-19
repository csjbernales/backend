using backend.api.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace backend.api.Data.Generated;

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
