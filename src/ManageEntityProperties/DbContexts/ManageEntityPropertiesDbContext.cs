using ManageEntityProperties.Entities;
using ManageEntityProperties.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ManageEntityProperties.DbContexts;

public class ManageEntityPropertiesDbContext : DbContext
{
    public ManageEntityPropertiesDbContext(DbContextOptions<ManageEntityPropertiesDbContext> options)
        : base(options)
    {
        // It's just a sample code. Don't use this in production.
        Database.Migrate();
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // log SQL queries to console...
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyCustomRules();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.ApplyCustomRules();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            List<Type> filterTypes = new();
            if (typeof(IEntityBase).IsAssignableFrom(entityType.ClrType))
            {
                filterTypes.Add(typeof(IEntityBase));
                entityType.ApplyActiveHandlerIndex();
            }
            entityType.ApplyQueryFilter(filterTypes);
        }

        modelBuilder
            .Entity<Product>(entity =>
            {
                entity
                    .HasKey(p => p.Id);
            });
        base.OnModelCreating(modelBuilder);
    }

}
