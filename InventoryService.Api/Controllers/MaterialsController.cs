using InventoryService.Application.Features.Materials.Commands.CreateMaterial;
using InventoryService.Application.Features.Materials.Queries.GetMaterialById;
using InventoryService.Application.Features.Materials.Queries.GetMaterials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MaterialsController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            var result = await _mediator.Send(new GetMaterialByIdQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet]
		public async Task<IActionResult> GetAllMaterials()
		{
			var result = await _mediator.Send(new GetMaterialsQuery());
            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialCommand command)
		{
            if (command == null)
                return BadRequest();

            var created = await _mediator.Send(command);
            if (!created.IsSuccess)
                return BadRequest(created.ErrorMessage);

            return CreatedAtAction(nameof(GetAllMaterials), new { id = created.Value?.Id }, created.Value);
		}
	}
}
