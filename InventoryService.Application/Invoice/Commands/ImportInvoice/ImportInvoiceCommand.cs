using MediatR;

namespace InventoryService.Application.Invoice.Commands.ImportInvoice
{
    public class ImportInvoiceCommand : IRequest<bool>
    {
        public Stream XmlStream { get; set; }
    }


}
