using BonefireCRM.Domain.Entities;
using System.Linq.Expressions;

namespace BonefireCRM.Domain.Services
{
    public static class QueryableFields
    {
        public static readonly Dictionary<string, Expression<Func<Contact, object>>> ContactMap = new()
        {
            ["id"] = x => x.Id,
            ["firstname"] = x => x.FirstName,
            ["lastname"] = x => x.LastName,
            ["email"] = x => x.Email,
        };

        public static IEnumerable<string> GetContactQueryableFieldNames()
            => ContactMap.Keys.AsEnumerable();
    }
}
