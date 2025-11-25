namespace InventoryService.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; } // PK
        public string InvoiceNumber { get; set; } // cbc:ID
        public DateTime IssueDate { get; set; } // cbc:IssueDate
        public DateTime? DueDate { get; set; } // cbc:DueDate
        public string InvoiceTypeCode { get; set; } // cbc:InvoiceTypeCode
        public string Currency { get; set; } // cbc:DocumentCurrencyCode
        public decimal LineExtensionAmount { get; set; } // cac:LegalMonetaryTotal/cbc:LineExtensionAmount
        public decimal TaxExclusiveAmount { get; set; } // cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount
        public decimal TaxInclusiveAmount { get; set; } // cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount
        public decimal PayableAmount { get; set; } // cac:LegalMonetaryTotal/cbc:PayableAmount

        // Supplier info
        public string SupplierName { get; set; } // cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name
        public string SupplierCompanyID { get; set; } // cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID

        public ICollection<Material> Materials { get; set; } = new List<Material>();
    }

}
