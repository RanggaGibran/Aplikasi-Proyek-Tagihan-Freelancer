using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
    Task<Invoice?> GetInvoiceByIdAsync(Guid id);
    Task<Invoice?> GetInvoiceWithItemsByIdAsync(Guid id);
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
    Task<bool> DeleteInvoiceAsync(Guid id);
    Task<IEnumerable<Invoice>> GetInvoicesByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Invoice>> GetInvoicesByStatusAsync(InvoiceStatus status);
    Task<bool> UpdateInvoiceStatusAsync(Guid invoiceId, InvoiceStatus status);
    Task<Invoice> CreateInvoiceFromProjectAsync(Guid projectId);
    Task<string> GenerateInvoiceNumberAsync();
    Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId);
}
