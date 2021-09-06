using System;
using SMS.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

namespace SMS.Core.Entities
{
	public class SaleEntity : ITrackable
	{
		[Key]
		public Guid Id { get; set; }

		public Guid ConsultantId { get; set; }

		public string UniqueNumber { get; set; }

		[ForeignKey(nameof(ConsultantId))]
		public virtual ConsultantEntity Consultant { get; set; }

		public virtual ICollection<SaleProductEntity> SalesProducts { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime? DateChanged { get; set; }

		public DateTime? DateDeleted { get; set; }
	}
}
