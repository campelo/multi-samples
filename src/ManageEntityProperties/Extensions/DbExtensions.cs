using ManageEntityProperties.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace ManageEntityProperties.Extensions;

public static class DbExtensions
{

    public static void ApplyCustomRules(this DbContext dbContext)
    {
        dbContext
            .ApplyRulesForAddedItems()
            .ApplyRulesForUpdatedItems()
            .ApplyRulesForDeletedItems();
    }

    public static DbContext ApplyRulesForAddedItems(this DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(EntityBase).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as EntityBase;
                entity.Status = "A";
            }
        }
        return dbContext;
    }

    public static DbContext ApplyRulesForUpdatedItems(this DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(EntityBase).IsAssignableFrom(entryType))
            {
                var entity = entry.Entity as EntityBase;
                entity.ModifiedOn = DateTime.UtcNow;
            }
        }
        return dbContext;
    }

    public static DbContext ApplyRulesForDeletedItems(this DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
        {
            Type entryType = entry.Entity.GetType();
            if (typeof(EntityBase).IsAssignableFrom(entryType))
            {
                entry.State = EntityState.Modified;
                var entity = entry.Entity as EntityBase;
                if (entity is not null)
                    entity.Status = "I";
            }
        }
        return dbContext;
    }

    public static void ApplyActiveHandlerIndex(this IMutableEntityType entity)
    {
        entity.AddIndex(entity.FindProperty(nameof(EntityBase.Status)));
    }

    public static void ApplyQueryFilter(this IMutableEntityType entity, List<Type> filterTypes)
    {
        var methodToCall = typeof(DbExtensions)
            .GetMethod(nameof(ApplyFilterConditions),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entity.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { filterTypes });
        if (filter == null)
            return;
        entity.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression ApplyFilterConditions<TEntity>(List<Type> filterTypes)
        where TEntity : class
    {
        List<Expression<Func<TEntity, bool>>> expressions = new();
        if (filterTypes.Contains(typeof(EntityBase)))
            expressions.Add(SetupActiveHandlerQueryFilter<TEntity>());

        if (!expressions.Any())
            return null;

        Expression<Func<TEntity, bool>> result = expressions.First();
        for (int i = 1; i < expressions.Count(); i++)
        {
            result = result.And(expressions[i]);
        }
        return result;
    }

    private static Expression<Func<TEntity, bool>> SetupActiveHandlerQueryFilter<TEntity>()
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> filter = x => (x as EntityBase).Status == "A";
        return filter;
    }
}
