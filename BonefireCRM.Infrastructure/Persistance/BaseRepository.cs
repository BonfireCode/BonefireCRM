﻿using BonefireCRM.Domain.Infrastructure.Persistance;
using LanguageExt;
using System.Linq.Expressions;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CRMContext _context;

        public BaseRepository(CRMContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, object>> sortExpression,
            string sortDirection,
            int skip,
            int take,
            CancellationToken ct)
        {
            var query = _context.Set<T>()
                .Where(filterExpression);

            query = sortDirection.Equals("DESC", StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(sortExpression)
                : query.OrderBy(sortExpression);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return query.AsEnumerable();
        }

        public async Task<T?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _context.FindAsync<T>(id, ct);
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct)
        {
            await _context.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken ct)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(ct);

            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken ct)
        {
            _context.Attach(entity);
            _context.Remove(entity);
            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            await _context.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);

            return entities;
        }
    }
}
