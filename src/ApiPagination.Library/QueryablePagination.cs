using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPagination.Library
{
    public static class QueryablePagination
    {
        public static IQueryable<T> MakePagination<T>(Func<SkipTake, IEnumerable<T>> callApi)
        {
            BaseApiPaginationQueryPagination<T> provider = new ApiPaginationQueryProvider<T>(callApi);
            return new ApiPaginationQuery<T>(provider);
        }

        public static IQueryable<T> MakePagination<T>(Func<SkipTake, Task<IEnumerable<T>>> callApi)
        {
            BaseApiPaginationQueryPagination<T> provider = new ApiPaginationQueryProviderAsync<T>(callApi);
            return new ApiPaginationQuery<T>(provider);
        }
    }
}