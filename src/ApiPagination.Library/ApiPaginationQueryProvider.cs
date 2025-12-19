using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("ApiPagination.Tests")]
namespace ApiPagination.Library
{
    internal class ApiPaginationQueryProvider<T> : BaseApiPaginationQueryPagination<T>
    {
        private readonly Func<SkipTake, IEnumerable<T>> apiCall;
        public ApiPaginationQueryProvider(Func<SkipTake, IEnumerable<T>> apiCall)
        {
            this.apiCall = apiCall;
        }

        protected override IEnumerable<T> getData(SkipTake skipTake) =>
            apiCall(skipTake);
    }
}