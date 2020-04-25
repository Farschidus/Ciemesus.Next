using System.Collections.Generic;

namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public class FilteredList<T> : List<T>
    {
        public FilteredList()
        {
        }

        public FilteredList(List<T> items) : base(items)
        {
        }
    }
}
