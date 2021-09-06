using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Common.Utilities
{
	public static class GuidExtension
	{
		public static bool IsNullOrEmpty(this Guid? guid)
		{
			return (!guid.HasValue || guid.Value == Guid.Empty);
		}
	}
}
