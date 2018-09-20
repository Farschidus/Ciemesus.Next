namespace Ciemesus.Core.Data
{
    public class DatabaseFactory : Disposable
    {
        private CiemesusDb _db;

        public CiemesusDb Get()
        {
            return _db ?? (_db = new CiemesusDb());
        }

        protected override void DisposeCore()
        {
            _db?.Dispose();
        }
    }
}
