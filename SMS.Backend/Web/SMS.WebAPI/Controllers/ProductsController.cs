using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMS.Business.Filters;
using SMS.Common.Models.Filter;
using SMS.Common.Utilities;
using SMS.Core.Interfaces.Services;
using SMS.Core.Models.Products.Request;
using SMS.Core.Models.Products.Response;

namespace SMS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService productService;
		public ProductsController(IProductService productService)
		{
			this.productService = productService;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] GlobalFilter filter)
		{
			var productsList = await this.productService.GetAllAsync();

			var products = ProductFilter.Filter(productsList, filter);

			var result = PaginatedList<ProductResponse>.Create(
				products.ToList(), filter.PageNumber ?? 1, filter.PageSize ?? 20
				);

			return Ok(result);
		}

		[HttpGet]
		[Route("GetProduct/{id}")]
		public async Task<IActionResult> GetConsultant(Guid id)
		{
			var response = await this.productService.GetProduct(id);

			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var product = response.Models.FirstOrDefault();
			return Ok(product);
		}

		[HttpPost]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
		{
			var response = await this.productService.CreateProductAsync(request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var createdProduct = response.Models.FirstOrDefault();
			return Ok(createdProduct);
		}

		[HttpPut]
		[Route("UpdateProduct/{id}")]
		public async Task<IActionResult> UpdateProduct(Guid id, ProductRequest request)
		{
			var response = await this.productService.UpdateProductAsync(id, request);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}

			var updatedProduct = response.Models.FirstOrDefault();
			return Ok(updatedProduct);
		}

		[HttpDelete]
		[Route("DeleteProduct/{id}")]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			var response = await this.productService.DeleteProductAsync(id);
			if (!response.Succeded)
			{
				return BadRequest(response.ToString());
			}
			return Ok();
		}

	}
}
