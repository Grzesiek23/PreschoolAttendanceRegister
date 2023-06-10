namespace PAR.Contracts.Requests.Options;

public class PagedRequest
{
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? SortField { get; init; }
    public string? SortOrder { get; init; }
}