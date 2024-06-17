using backend.api.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace backend.api.Data.Generated;

/// <summary>
/// dbcontext
/// </summary>
public partial class FullstackDBContext : DbContext
{
    /// <summary>
    /// dbcontext controller
    /// </summary>
    public FullstackDBContext()
    {
    }

    /// <summary>
    /// dbcontext controller w/ non empty context
    /// </summary>
    /// <param name="options"></param>
    public FullstackDBContext(DbContextOptions<FullstackDBContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Carts
    /// </summary>
    public virtual DbSet<Cart> Carts { get; set; }

    /// <summary>
    /// Customer
    /// </summary>
    public virtual DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Products
    /// </summary>
    public virtual DbSet<Product> Products { get; set; }

    /// <summary>
    /// OnModelCreating
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
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
