using System;
using System.ComponentModel.DataAnnotations;

namespace SMS.Core.Interfaces
{
	public interface ITrackable
	{
		[Key]
		Guid Id { get; set; }

		DateTime DateCreated { get; set; }

		DateTime? DateChanged { get; set; }

		DateTime? DateDeleted { get; set; }
	}
}
