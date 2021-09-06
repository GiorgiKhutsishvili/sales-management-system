using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMS.Core.Models.Consultants.Response;
using SMS.Core.Models.Consultants.Request;
using SMS.Core.Models;

namespace SMS.Core.Interfaces.Services
{
	public interface IConsultantService
	{
		Task<GenericResponse<ConsultantResponse>> GetConsultant(Guid id);

		Task<IEnumerable<ConsultantResponse>> GetAllAsync();

		Task<GenericResponse<ConsultantResponse>> CreateConsultantAsync(ConsultantRequest request);

		Task<GenericResponse<ConsultantResponse>> UpdateConsultantAsync(Guid id, ConsultantRequest request);

		Task<Response> DeleteConsultantAsync(Guid id);
	}
}
