using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ApiPagination.Tests")]
namespace ApiPagination.Library
{
    internal class ApiPaginationQueryProviderAsync<T> : BaseApiPaginationQueryPagination<T>
    {
        private readonly Func<int, int, Task<IEnumerable<T>>> apiCall;

        public ApiPaginationQueryProviderAsync(Func<int, int, Task<IEnumerable<T>>> apiCall)
        {
            this.apiCall = apiCall;
        }

        protected override IEnumerable<T> getData(int skip, int take)
        {
            return apiCall(skip, take)
                .GetAwaiter()
                .GetResult();
        }
    }
}