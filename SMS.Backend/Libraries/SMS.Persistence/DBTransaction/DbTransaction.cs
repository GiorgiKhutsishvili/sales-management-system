using Microsoft.EntityFrameworkCore.Storage;

namespace SMS.Persistence.DBTransaction
{
	public class DbTransaction : ITransaction
	{
		private readonly IDbContextTransaction _transaction;
		public DbTransaction(IDbContextTransaction transaction)
		{
			_transaction = transaction;
		}
        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
