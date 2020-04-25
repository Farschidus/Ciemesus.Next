namespace Ciemesus.Core.Data.DbQueries
{
    public interface IDbQuery<out TResult>
    {
        TResult Execute(CiemesusDb db);
    }
}
