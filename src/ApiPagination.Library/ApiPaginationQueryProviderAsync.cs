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
        private readonly Func<SkipTake, Task<IEnumerable<T>>> apiCall;

        public ApiPaginationQueryProviderAsync(Func<SkipTake, Task<IEnumerable<T>>> apiCall)
        {
            this.apiCall = apiCall;
        }

        protected override IEnumerable<T> getData(SkipTake skipTake)
        {
            return apiCall(skipTake)
                .GetAwaiter()
                .GetResult();
        }
    }
}