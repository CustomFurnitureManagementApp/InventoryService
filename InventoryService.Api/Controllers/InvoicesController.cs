using InventoryService.Application.Invoice.Commands.ImportInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            using var stream = file.OpenReadStream();
            var command = new ImportInvoiceCommand { XmlStream = stream };

            var result = await _mediator.Send(command);
            return result ? Ok("Invoice imported successfully") : BadRequest("Failed to import invoice");
        }
    }

}
