using Microsoft.EntityFrameworkCore;
using PAR.Contracts;
using PAR.Contracts.Requests.Options;
using System.Linq.Dynamic.Core;
using PAR.Application.Exceptions;

namespace PAR.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResponse<TDto>> ToPagedResponseAsync<TEntity, TDto>(this IQueryable<TEntity> query,
        PagedRequest request, Func<TEntity, TDto> mapper, CancellationToken cancellationToken = default)
    {
        var total = await query.CountAsync(cancellationToken);

        try
        {
            if (request.SortField != null)
                query = query.OrderBy($"{request.SortField} {request.SortOrder}");
        }
        catch (System.Linq.Dynamic.Core.Exceptions.ParseException ex)
        {
            throw new InternalApplicationError($"Invalid sort field: {request.SortField}", nameof(ToPagedResponseAsync), ex);
        }

        var pagedQuery = query.Skip((request.Page ?? 0) * (request.PageSize ?? 30)).Take(request.PageSize ?? 30);
        var entities = await pagedQuery.ToListAsync(cancellationToken);

        return new PagedResponse<TDto>
        {
            Items = entities.Select(mapper),
            Page = request.Page ?? 0,
            PageSize = request.PageSize ?? 30,
            TotalCount = total
        };
    }
}