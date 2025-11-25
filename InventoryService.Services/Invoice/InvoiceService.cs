using InventoryService.Domain.Entities;
using InventoryService.Repositories;
using InventoryService.Services.Invoice;
using System.Xml;

namespace InventoryService.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMaterialRepository _materialRepo;
        private readonly IStockItemRepository _stockRepo;

        public InvoiceService(
            IInvoiceRepository invoiceRepo,
            IMaterialRepository materialRepo,
            IStockItemRepository stockRepo)
        {
            _invoiceRepo = invoiceRepo;
            _materialRepo = materialRepo;
            _stockRepo = stockRepo;
        }

        public async Task<bool> ImportInvoiceAsync(Stream xmlStream)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlStream);

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");

            // 1️⃣ Creăm Invoice-ul
            var invoice = new Domain.Entities.Invoice
            {
                InvoiceNumber = xmlDoc.SelectSingleNode("//cbc:ID", nsmgr)?.InnerText ?? "UNKNOWN",
                IssueDate = DateTime.Parse(xmlDoc.SelectSingleNode("//cbc:IssueDate", nsmgr)?.InnerText ?? DateTime.UtcNow.ToString()),
                DueDate = DateTime.TryParse(xmlDoc.SelectSingleNode("//cbc:DueDate", nsmgr)?.InnerText, out var due) ? due : null,
                InvoiceTypeCode = xmlDoc.SelectSingleNode("//cbc:InvoiceTypeCode", nsmgr)?.InnerText ?? "",
                Currency = xmlDoc.SelectSingleNode("//cbc:DocumentCurrencyCode", nsmgr)?.InnerText ?? "RON",
                LineExtensionAmount = decimal.Parse(xmlDoc.SelectSingleNode("//cac:LegalMonetaryTotal/cbc:LineExtensionAmount", nsmgr)?.InnerText ?? "0"),
                TaxExclusiveAmount = decimal.Parse(xmlDoc.SelectSingleNode("//cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount", nsmgr)?.InnerText ?? "0"),
                TaxInclusiveAmount = decimal.Parse(xmlDoc.SelectSingleNode("//cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount", nsmgr)?.InnerText ?? "0"),
                PayableAmount = decimal.Parse(xmlDoc.SelectSingleNode("//cac:LegalMonetaryTotal/cbc:PayableAmount", nsmgr)?.InnerText ?? "0"),
                SupplierName = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name", nsmgr)?.InnerText ?? "",
                SupplierCompanyID = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", nsmgr)?.InnerText ?? ""
            };

            // Salvăm factura
            await _invoiceRepo.AddInvoiceAsync(invoice);

            // 2️⃣ Procesăm liniile facturii
            var lineNodes = xmlDoc.SelectNodes("//cac:InvoiceLine", nsmgr);
            foreach (XmlNode lineNode in lineNodes!)
            {
                var materialName = lineNode.SelectSingleNode("cac:Item/cbc:Name", nsmgr)?.InnerText;
                var priceText = lineNode.SelectSingleNode("cac:Price/cbc:PriceAmount", nsmgr)?.InnerText;
                var quantityText = lineNode.SelectSingleNode("cbc:InvoicedQuantity", nsmgr)?.InnerText;

                if (string.IsNullOrEmpty(materialName) || string.IsNullOrEmpty(priceText)) continue;

                var price = decimal.Parse(priceText);
                var quantity = decimal.TryParse(quantityText, out var q) ? q : 0;

                // 3️⃣ Upsert Material
                var material = await _materialRepo.GetByNameAndPriceAsync(materialName, price);
                if (material != null)
                {
                    // Material existent → actualizează stoc
                    var stockItem = await _stockRepo.GetByMaterialIdAsync(material.Id, warehouseId: 1);
                    if (stockItem != null)
                    {
                        stockItem.Quantity += quantity;
                        await _stockRepo.UpdateAsync(stockItem);
                    }
                }
                else
                {
                    // Material nou
                    material = new Material
                    {
                        Name = materialName,
                        UnitCost = price,
                        Invoice = invoice // legăm materialul de factura curentă
                    };
                    await _materialRepo.AddAsync(material);

                    // Creăm StockItem pentru materialul nou
                    var stockItem = new StockItem
                    {
                        ProductVariantId = material.Id,
                        WarehouseId = 1,
                        Quantity = quantity,
                        ReservedQuantity = 0,
                        AverageCost = price
                    };
                    await _stockRepo.AddAsync(stockItem);
                }

                // 4️⃣ Adăugăm materialul în Invoice
                invoice.Materials.Add(material);
            }

            // 5️⃣ Salvăm update-ul Invoice cu liniile materiale
            await _invoiceRepo.UpdateInvoiceAsync(invoice);
            return true;
        }
    }

}
