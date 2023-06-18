export interface PagedResponse<TResponse> {
    items: TResponse[];
    page: number;
    pageSize: number;
    totalCount: number;
    hasNextPage: boolean;
}