using FilterPagingEfCore.Enums;
using FilterPagingEfCore.Filter;
using FilterPagingEfCore.Paging;
using FilterPagingEfCore.Sort;

public static class PagingParamFactory
{
    /// <summary>
    /// Creates a PagingParam with default settings.
    /// </summary>
    public static PagingParam CreateDefault()
    {
        return new PagingParam
        {
            PageNumber = 1,
            PageSize = 10,
            SortingParams = new List<SortingParams>(),
            FilterParam = new List<FilterParam>(),
            ComparisonMode = ComparisonMode.And
        };
    }
    public static PagingParam CreateAllRecords()
    {
        return new PagingParam
        {
            PageNumber = 1,
            PageSize = -1,
            SortingParams = new List<SortingParams>(),
            FilterParam = new List<FilterParam>(),
            ComparisonMode = ComparisonMode.And
        };
    }

    /// <summary>
    /// Creates a PagingParam with custom paging options.
    /// </summary>
    public static PagingParam Create(int pageNumber, int pageSize)
    {
        return new PagingParam
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortingParams = new List<SortingParams>(),
            FilterParam = new List<FilterParam>(),
            ComparisonMode = ComparisonMode.And
        };
    }

    /// <summary>
    /// Creates a PagingParam with filtering and sorting.
    /// </summary>
    public static PagingParam CreateWithFiltersAndSorting(
        int pageNumber,
        int pageSize,
        List<FilterParam> filters,
        List<SortingParams> sortings,
        ComparisonMode comparisonMode = ComparisonMode.And)
    {
        return new PagingParam
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            FilterParam= filters ?? new List<FilterParam>(),
            SortingParams = sortings ?? new List<SortingParams>(),
            ComparisonMode = comparisonMode
        };
    }
}
