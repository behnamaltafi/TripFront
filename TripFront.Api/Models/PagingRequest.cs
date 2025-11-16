using FilterPagingEfCore.Enums;
using FilterPagingEfCore.Filter;
using FilterPagingEfCore.Paging;
using FilterPagingEfCore.Sort;

namespace TripFront.Api.Models;

public class PagingRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public List<FilterParam>? Filters { get; set; }
    public List<SortingParams>? Sorting { get; set; }
    public ComparisonMode ComparisonMode { get; set; } = ComparisonMode.And;

    public PagingParam ToPagingParam()
    {
        return new PagingParam
        {
            PageNumber = PageNumber,
            PageSize = PageSize,
            FilterParam = Filters ?? new List<FilterParam>(),
            SortingParams = Sorting ?? new List<SortingParams>(),
            ComparisonMode = ComparisonMode
        };
    }
}
