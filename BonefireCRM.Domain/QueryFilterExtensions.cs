using BonefireCRM.Domain.DTOs.Shared;
using System.Globalization;
using System.Linq.Expressions;

namespace BonefireCRM.Domain
{
    internal static class QueryFilterExtensions
    {
        public static IEnumerable<string> GetSupportedOperators()
        {
            return
            [
                "equals: Checks if the field is equal to the value",
                "notequals: Checks if the field is not equal to the value",
                "greaterthan: Checks if the field is greater than the value",
                "greaterthanorequal: Checks if the field is greater than or equal to the value",
                "lessthan: Checks if the field is less than the value",
                "lessthanorequal: Checks if the field is less than or equal to the value",
                "contains: Checks if the string contains the value",
                "startswith: Checks if the string starts with the value",
                "endswith: Checks if the string ends with the value",
                "isempty: Checks if the string is empty",
                "isnotempty: Checks if the string is not empty",
                "isnull: Checks if the field is null",
                "isnotnull: Checks if the field is not null"
            ];
        }

        public static (Expression<Func<T, bool>>? Predicate,
                  Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy,
                  int? Skip,
                  int? Take)
        BuildQueryComponents<T>(
            FilterRequestDTO filterRequest,
            Dictionary<string, Expression<Func<T, object>>> fieldMap)
        {
            Expression<Func<T, bool>>? predicate = null;
            if (filterRequest.Filter != null)
            {
                predicate = BuildGroupExpression<T>(filterRequest.Filter, fieldMap);
            }

            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null;
            if (filterRequest.Sort != null && filterRequest.Sort.Any())
            {
                orderBy = q => ApplySorting(q, filterRequest.Sort, fieldMap);
            }

            var page = filterRequest.Page <= 0 ? 1 : filterRequest.Page;
            var pageSize = filterRequest.PageSize <= 0 ? 10 : filterRequest.PageSize;
            int skip = (page - 1) * pageSize;
            int take = pageSize;

            return (predicate, orderBy, skip, take);
        }

        private static Expression<Func<T, bool>> BuildGroupExpression<T>(
            FilterGroup group,
            Dictionary<string, Expression<Func<T, object>>> fieldMap)
        {
            Expression? combined = null;
            var param = Expression.Parameter(typeof(T), "x");

            foreach (var filter in group.Filters)
            {
                if (!fieldMap.TryGetValue(filter.Field, out var selector))
                    continue;

                var member = ReplaceParameter(selector.Body, selector.Parameters[0], param);
                var constant = Expression.Constant(ConvertValue(filter.Value, member.Type), member.Type);
                var condition = BuildCondition((MemberExpression)ToMemberExpression(member), constant, filter.Operator);

                combined = combined == null
                    ? condition
                    : CombineExpressions(combined, condition, group.Logic);
            }

            combined ??= Expression.Constant(false);

            return Expression.Lambda<Func<T, bool>>(combined, param);
        }

        private static Expression ReplaceParameter(Expression body, ParameterExpression from, ParameterExpression to) =>
            new ReplaceVisitor(from, to).Visit(body)!;

        private class ReplaceVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _from, _to;
            public ReplaceVisitor(ParameterExpression from, ParameterExpression to) { _from = from; _to = to; }
            protected override Expression VisitParameter(ParameterExpression node) => node == _from ? _to : base.VisitParameter(node);
        }

        private static Expression ToMemberExpression(Expression expr)
            => expr is UnaryExpression unary ? unary.Operand : expr;

        private static Expression BuildCondition(MemberExpression member, ConstantExpression constant, string op)
        {
            var targetType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

            object? value = constant.Value;

            // Convert to proper type
            if (targetType.IsEnum)
                value = Enum.Parse(targetType, constant.Value!.ToString()!);
            else if (targetType == typeof(Guid))
                value = constant.Value is Guid g ? g : Guid.Parse(constant.Value!.ToString()!);
            else if (constant.Value != null)
                value = Convert.ChangeType(constant.Value, targetType);

            var typedConstant = Expression.Constant(value, targetType);

            return op.ToLower() switch
            {
                "equals" => Expression.Equal(member, typedConstant),
                "notequals" => Expression.NotEqual(member, typedConstant),
                "greaterthan" => Expression.GreaterThan(member, typedConstant),
                "greaterthanorequal" => Expression.GreaterThanOrEqual(member, typedConstant),
                "lessthan" => Expression.LessThan(member, typedConstant),
                "lessthanorequal" => Expression.LessThanOrEqual(member, typedConstant),

                // String operations
                "contains" when targetType == typeof(string)
                    => Expression.Call(member, nameof(string.Contains), Type.EmptyTypes, typedConstant),
                "startswith" when targetType == typeof(string)
                    => Expression.Call(member, nameof(string.StartsWith), Type.EmptyTypes, typedConstant),
                "endswith" when targetType == typeof(string)
                    => Expression.Call(member, nameof(string.EndsWith), Type.EmptyTypes, typedConstant),
                "isempty" when targetType == typeof(string)
                    => Expression.Equal(member, Expression.Constant(string.Empty)),
                "isnotempty" when targetType == typeof(string)
                    => Expression.NotEqual(member, Expression.Constant(string.Empty)),

                // Null checks
                "isnull" => Expression.Equal(member, Expression.Constant(null, member.Type)),
                "isnotnull" => Expression.NotEqual(member, Expression.Constant(null, member.Type)),

                _ => Expression.Constant(true)
            };
        }


        private static Expression CombineExpressions(Expression left, Expression right, string logic) =>
            logic.ToLower() == "or"
                ? Expression.OrElse(left, right)
                : Expression.AndAlso(left, right);

        private static object? ConvertValue(string? value, Type targetType)
        {
            if (value == null) return null;

            targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (targetType == typeof(string)) return value;
            if (targetType == typeof(bool)) return bool.Parse(value);
            if (targetType.IsEnum) return Enum.Parse(targetType, value, true);
            if (targetType == typeof(Guid)) return Guid.Parse(value);
            if (targetType == typeof(DateTime)) return DateTime.Parse(value, CultureInfo.InvariantCulture);
            if (targetType == typeof(int)) return int.Parse(value, CultureInfo.InvariantCulture);
            if (targetType == typeof(long)) return long.Parse(value, CultureInfo.InvariantCulture);
            if (targetType == typeof(decimal)) return decimal.Parse(value, CultureInfo.InvariantCulture);
            if (targetType == typeof(double)) return double.Parse(value, CultureInfo.InvariantCulture);

            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }

        private static IOrderedQueryable<T> ApplySorting<T>(
            IQueryable<T> query,
            List<SortDefinition> sorts,
            Dictionary<string, Expression<Func<T, object>>> fieldMap)
        {
            IOrderedQueryable<T>? ordered = null;
            bool first = true;

            foreach (var sort in sorts)
            {
                if (!fieldMap.TryGetValue(sort.Field, out var selector))
                    continue;

                if (first)
                {
                    ordered = sort.Direction.ToLower() == "desc"
                        ? query.OrderByDescending(selector)
                        : query.OrderBy(selector);

                    first = false;
                }
                else
                {
                    ordered = sort.Direction.ToLower() == "desc"
                        ? ordered!.ThenByDescending(selector)
                        : ordered!.ThenBy(selector);
                }
            }

            return ordered ?? query.OrderBy(x => 0);
        }
    }
}
