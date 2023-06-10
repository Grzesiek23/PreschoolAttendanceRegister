namespace PAR.Contracts;

public class PagedResponse<TResponse>
{
    public IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage => TotalCount > (Page + 1) * PageSize;  
}