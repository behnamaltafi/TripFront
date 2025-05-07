using AntDesign;
using AntDesign.TableModels;
using FilterPagingEfCore.Enums;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Filter;
using FilterPagingEfCore.Paging;
using FilterPagingEfCore.Sort;

namespace TripFront.Components.FrontUtility
{
    public static class MapFilter
    {
        public static ComparisonMethod ToComparisonMethod(this TableFilterCompareOperator op) => op switch
        {
            TableFilterCompareOperator.Equals => ComparisonMethod.Equal,
            TableFilterCompareOperator.NotEquals => ComparisonMethod.NotEqual,
            TableFilterCompareOperator.GreaterThan => ComparisonMethod.GreaterThan,
            TableFilterCompareOperator.LessThan => ComparisonMethod.LessThan,
            TableFilterCompareOperator.GreaterThanOrEquals => ComparisonMethod.GreaterThanEqual,
            TableFilterCompareOperator.LessThanOrEquals => ComparisonMethod.LessThanEqual,
            TableFilterCompareOperator.Contains => ComparisonMethod.Contain,
            TableFilterCompareOperator.NotContains => ComparisonMethod.NotContain,
            TableFilterCompareOperator.StartsWith => ComparisonMethod.StartWith,
            TableFilterCompareOperator.EndsWith => ComparisonMethod.EndWith,

            _ => ComparisonMethod.Equal
        };
        public static SortOrders ToSortOrder(this SortDirection sortOrder)
        {
            return sortOrder switch
            {
                SortDirection.Ascending => SortOrders.Asc,
                SortDirection.Descending => SortOrders.Desc,
                _ => SortOrders.Asc,
            };
        }


        public static PagingParam ToPagingParam<T>(this QueryModel<T> queryModel)
        {
            var filters = new List<FilterParam>();
            var sortingParams = new List<SortingParams>();

            if (queryModel.FilterModel != null)
            {
                foreach (var filter in queryModel.FilterModel)
                {
                    filters.Add(new FilterParam
                    {
                        ColumnName = filter.FieldName,
                        FilterValue = filter.SelectedValues?.FirstOrDefault()?.ToString() ?? "",
                        FilterOption = filter.Filters?.FirstOrDefault()?.FilterCompareOperator.ToComparisonMethod() ?? ComparisonMethod.Equal
                    });
                }
            }

            if (queryModel.SortModel != null)
            {
                foreach (var sort in queryModel.SortModel)
                {
                    sortingParams.Add(new SortingParams
                    {
                        ColumnName = sort.FieldName,
                        SortOrder = sort.SortDirection.ToSortOrder()
                    });
                }
            }

            return new PagingParam
            {
                PageNumber = queryModel.PageIndex,
                PageSize = queryModel.PageSize,
                FilterParam = filters,
                SortingParams = sortingParams
            };
        }
    }
}




