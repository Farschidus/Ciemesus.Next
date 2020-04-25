namespace Ciemesus.Core.Api.Infrastructure.Pagination
{
    public interface IFilterableInput<T>
    {
        string Filter { get; set; }
    }
}
