using System;

namespace SMS.Persistence.DBTransaction
{
	public interface ITransaction : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
