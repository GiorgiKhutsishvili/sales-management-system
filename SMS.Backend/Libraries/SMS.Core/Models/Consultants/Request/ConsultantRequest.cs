using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Consultants.Request
{
	public class ConsultantRequest
	{
		public Guid? RecommendatorId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PersonalId { get; set; }

		public string Gender { get; set; }

		public DateTime BirthDate { get; set; }
	}
}
