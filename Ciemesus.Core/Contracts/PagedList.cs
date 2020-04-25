using System;
using System.Collections.Generic;

namespace Ciemesus.Core.Contracts
{
    public class PagedList
    {
        public const int DefaultPageSize = 10;
    }

    public class PagedList<T> : PagedList
    {
        public PagedList()
        {
        }

        public PagedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; private set; } = 1;

        public int TotalCount { get; private set; } = 0;

        public int PageSize { get; private set; } = DefaultPageSize;

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 <= TotalPages;
            }
        }

        public int NextPageIndex
        {
            get
            {
                var nextPage = TotalPages;

                if (HasNextPage)
                {
                    nextPage = PageIndex + 1;
                }

                return nextPage;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        public int PreviousPageIndex
        {
            get
            {
                var previousPage = 1;

                if (HasPreviousPage)
                {
                    previousPage = PageIndex - 1;
                }

                return previousPage;
            }
        }

        public List<T> Items { get; private set; } = new List<T>();
    }
}
