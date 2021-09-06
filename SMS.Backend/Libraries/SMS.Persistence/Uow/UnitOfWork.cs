using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMS.Persistence.DBTransaction;

namespace SMS.Persistence.Uow
{
	public class UnitOfWork : IUnitOfWork
	{
		private DbContext context;

		public UnitOfWork(DbContext context)
		{
			this.context = context;
		}

		public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return new DbTransaction(this.context.Database.BeginTransaction());
		}

		public DbContext Context()
		{
			return this.context;
		}

		public void Commit()
		{
			this.context.SaveChanges();
		}

		public async Task CommitAsync()
		{
			await this.context.SaveChangesAsync();
		}

		public void Dispose()
		{
			this.context = null;
		}
	}
}
