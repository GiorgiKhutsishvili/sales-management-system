using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Persistence.Uow
{
	public interface IUnitOfWork : IDisposable
	{
		DbContext Context();
		void Commit();
		Task CommitAsync();
	}
}
