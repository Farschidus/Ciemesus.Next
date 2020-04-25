namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public interface IPaginationInput<T> : IFilterableInput<T>
    {
        int First { get; set; }
        object After { get; set; }
        object Before { get; set; }
        bool IsAsc { get; set; }
        string[] OrderBy { get; set; }
    }
}
