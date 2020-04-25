using System.Collections.Generic;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public class Pagination<T>
    {
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
