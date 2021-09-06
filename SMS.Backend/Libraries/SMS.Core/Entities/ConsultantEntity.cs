using System;
using SMS.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Entities
{
	public class ConsultantEntity : ITrackable
	{
		public Guid Id { get; set; }

		public Guid? RecommendatorId { get; set; }

		public string UniqueNumber { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PersonalId { get; set; }

		public string Gender { get; set; }

		public DateTime BirthDate { get; set; }
		public string RecommendatorUniqueNumber { get; set; }

		public virtual ICollection<SaleEntity> Sales { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime? DateChanged { get; set; }

		public DateTime? DateDeleted { get; set; }
	}
}

