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
        private readonly Func<int, int, IEnumerable<T>> apiCall;
        public ApiPaginationQueryProvider(Func<int, int, IEnumerable<T>> apiCall)
        {
            this.apiCall = apiCall;
        }

        protected override IEnumerable<T> getData(int skip, int take) =>
            apiCall(skip, take);
    }
}