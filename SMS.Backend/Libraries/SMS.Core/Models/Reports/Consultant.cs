using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Reports
{
	public class Consultant
	{
		public int Index { get; set; }

		public Guid Id { get; set; }

		public string ConsultantUniqueNumber { get; set; }

		public string ConsultantFullName { get; set; }

		public string ConsultantPersonalId { get; set; }

		public string ConsultantBirthDate { get; set; }
	}
}
