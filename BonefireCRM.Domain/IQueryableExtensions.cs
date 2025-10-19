using System.Linq.Expressions;

namespace BonefireCRM.Domain
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var method = typeof(Queryable).GetMethods()
                .Single(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IOrderedQueryable<T>)method.Invoke(null, [source, lambda])!;
        }

        public static IOrderedQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var method = typeof(Queryable).GetMethods()
                .Single(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IOrderedQueryable<T>)method.Invoke(null, [source, lambda])!;
        }
    }
}
