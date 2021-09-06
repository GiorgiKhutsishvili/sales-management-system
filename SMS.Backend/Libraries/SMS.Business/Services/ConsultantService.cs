using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMS.Core.Entities;
using SMS.Core.Interfaces;
using SMS.Core.Interfaces.Services;
using SMS.Persistence.Uow;
using SMS.Core.Models.Consultants.Response;
using SMS.Core.Models.Consultants.Request;
using SMS.Core.Models;
using SMS.Business.Resources;

namespace SMS.Business.Services
{
	public class ConsultantService : IConsultantService
	{
		private readonly IUnitOfWork unitOfWork;
		public ConsultantService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<GenericResponse<ConsultantResponse>> GetConsultant(Guid id)
		{
			var response = new GenericResponse<ConsultantResponse>();

			var consultant = await Get(id);
			if (consultant == null)
			{
				response.AddError(string.Format(SharedResource.Errors_ConsultantIsNotFound, id));
				return response;
			}

			var consultantResponse = new ConsultantResponse(consultant, null);
			response.Models.Add(consultantResponse);
			return response;
		}

		public async Task<IEnumerable<ConsultantResponse>> GetAllAsync()
		{
			var query = await this.BaseQuery();

			var consultants = query.Select((index, entity) =>
				new ConsultantResponse(index, entity));

			return consultants;
		}
		public async Task<GenericResponse<ConsultantResponse>> CreateConsultantAsync(ConsultantRequest request)
		{
			var genericResponse = new GenericResponse<ConsultantResponse>();

			var consultants = this.unitOfWork.Context()
				.Set<ConsultantEntity>()
				.Where(c =>
					c.PersonalId == request.PersonalId &&
						c.DateDeleted == null);

			if (await consultants.AnyAsync())
			{
				genericResponse.AddError(string.Format(SharedResource.Errors_ConsultantAlreadyExists, request.PersonalId));
				return genericResponse;
			}

			ConsultantEntity recommendator = null;
			if (request.RecommendatorId.HasValue)
			{
				recommendator = await Get(request.RecommendatorId.Value);
				if (recommendator == null)
				{
					genericResponse.AddError(string.Format(SharedResource.Errors_RecommendatorNotFound, request.RecommendatorId));
					return genericResponse;
				}
			}

			var uniqueNumber = (uint)Guid.NewGuid().GetHashCode();
			var consultantEntity = new ConsultantEntity
			{
				Id = Guid.NewGuid(),
				RecommendatorId = recommendator?.Id,
				UniqueNumber = uniqueNumber.ToString(),
				FirstName = request.FirstName,
				LastName = request.LastName,
				PersonalId = request.PersonalId,
				Gender = request.Gender,
				BirthDate = request.BirthDate.Date,
				RecommendatorUniqueNumber = recommendator?.UniqueNumber,
				DateCreated = DateTime.Now
			};

			await this.unitOfWork.Context().AddAsync(consultantEntity);
			await this.unitOfWork.CommitAsync();

			var response = new ConsultantResponse(consultantEntity, null);
			genericResponse.Models.Add(response);
			return genericResponse;
		}

		public async Task<GenericResponse<ConsultantResponse>> UpdateConsultantAsync(Guid id, ConsultantRequest request)
		{
			var response = new GenericResponse<ConsultantResponse>();

			var consultant = await Get(id);
			if (consultant == null)
			{
				response.AddError(string.Format(SharedResource.Errors_ConsultantIsNotFound, id));
				return response;
			}

			if (consultant.Id == request.RecommendatorId)
			{
				response.AddError(SharedResource.Errors_ConsultantRepresentsHisOwnRecommender);
				return response;
			}

			consultant.RecommendatorId = request.RecommendatorId;
			consultant.PersonalId = request.PersonalId;
			consultant.FirstName = request.FirstName;
			consultant.LastName = request.LastName;
			consultant.Gender = request.Gender;
			consultant.BirthDate = request.BirthDate;
			consultant.DateChanged = DateTime.Now;

			await this.unitOfWork.CommitAsync();

			var consultantResponse = new ConsultantResponse(consultant, null);
			response.Models.Add(consultantResponse);
			return response;
		}


		public async Task<Response> DeleteConsultantAsync(Guid id)
		{
			var response = new Response();

			var consultant = await Get(id);
			if (consultant == null)
			{
				response.AddError(string.Format(SharedResource.Errors_ConsultantIsNotFound, id));
				return response;
			}

			consultant.DateDeleted = DateTime.Now;
			await this.unitOfWork.CommitAsync();

			return response;
		}

		#region Private Methods
		private async Task<ConsultantEntity> Get(Guid id)
		{
			var consultant = await this.unitOfWork.Context()
				.Set<ConsultantEntity>()
				.FirstOrDefaultAsync(c => c.Id == id);

			return consultant;
		}

		private async Task<IEnumerable<ConsultantEntity>> BaseQuery()
		{
			var query = await this.unitOfWork.Context()
				.Set<ConsultantEntity>()
				.AsNoTracking()
				.Include(c => c.Sales)
				.Where(d => d.DateDeleted == null)
				.ToListAsync();

			return query;
		}
		#endregion
	}
}
