using InventoryService.Application.Features.Products.Commands.CreateProduct;
using InventoryService.Application.Features.Products.Queries.GetProductById;
using InventoryService.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

        [HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetProductsQuery());
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _mediator.Send(new GetProductByIdQuery(id));
			if (result is null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
		{
			var created = await _mediator.Send(command);
			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}
	}
}
