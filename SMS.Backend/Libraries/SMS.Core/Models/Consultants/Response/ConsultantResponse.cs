using SMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core.Models.Consultants.Response
{
	public class ConsultantResponse
	{
		public ConsultantResponse(ConsultantEntity consultant, int? index)
		{
			this.Index = index.HasValue ? index + 1 : 0;
			this.Id = consultant.Id;
			this.RecommendatorId = consultant.RecommendatorId;
			this.UniqueNumber = consultant.UniqueNumber;
			this.FirstName = consultant.FirstName;
			this.LastName = consultant.LastName;
			this.PersonalId = consultant.PersonalId;
			this.Gender = consultant.Gender;
			this.BirthDate = consultant.BirthDate.ToString("yyyy-MM-dd");
			this.RecommendatorUniqueNumber = consultant.RecommendatorUniqueNumber;
			this.DateCreated = consultant.DateCreated.ToString("yyyy-MM-dd");
		}

		public int? Index { get; set; }

		public Guid Id { get; set; }

		public Guid? RecommendatorId { get; set; }

		public string UniqueNumber { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PersonalId { get; set; }

		public string Gender { get; set; }

		public string BirthDate { get; set; }

		public string RecommendatorUniqueNumber { get; set; }

		public string DateCreated { get; set; }
	}
}
