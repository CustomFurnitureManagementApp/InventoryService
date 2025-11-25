using InventoryService.Services.Invoice;
using MediatR;

namespace InventoryService.Application.Invoice.Commands.ImportInvoice
{
    public class ImportInvoiceCommandHandler : IRequestHandler<ImportInvoiceCommand, bool>
    {
        private readonly IInvoiceService _invoiceService;

        public ImportInvoiceCommandHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<bool> Handle(ImportInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.XmlStream == null)
                throw new ArgumentException("Stream is null");

            // delegăm parsing-ul și salvarea către serviciu
            return await _invoiceService.ImportInvoiceAsync(request.XmlStream);
        }
    }
}
