using InventoryService.Application.Features.Material.Queries.GetMaterials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MaterialsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

        [HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetMaterialsQuery());
			return Ok(result);
		}

		//[HttpPost]
		//public async Task<IActionResult> Create([FromBody] CreateMaterialCommand command)
		//{
		//	var created = await _mediator.Send(command);
		//	return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		//}
	}
}
