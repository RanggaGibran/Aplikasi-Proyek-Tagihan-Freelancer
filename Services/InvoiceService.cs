using Microsoft.EntityFrameworkCore;
using Aplikasi_Proyek___Tagihan_Freelancer.Data;
using Aplikasi_Proyek___Tagihan_Freelancer.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext _context;

    public InvoiceService(ApplicationDbContext context)
    {
        _context = context;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        return await _context.Invoices
            .Include(i => i.Project)
                .ThenInclude(p => p.Client)
            .Include(i => i.InvoiceItems)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
    {
        return await _context.Invoices
            .Include(i => i.Project)
                .ThenInclude(p => p.Client)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Invoice?> GetInvoiceWithItemsByIdAsync(Guid id)
    {
        return await _context.Invoices
            .Include(i => i.Project)
                .ThenInclude(p => p.Client)
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
        if (string.IsNullOrEmpty(invoice.InvoiceNumber))
        {
            invoice.InvoiceNumber = await GenerateInvoiceNumberAsync();
        }

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
        return invoice;
    }

    public async Task<bool> DeleteInvoiceAsync(Guid id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null) return false;

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByProjectIdAsync(Guid projectId)
    {
        return await _context.Invoices
            .Include(i => i.Project)
                .ThenInclude(p => p.Client)
            .Include(i => i.InvoiceItems)
            .Where(i => i.ProjectId == projectId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByStatusAsync(InvoiceStatus status)
    {
        return await _context.Invoices
            .Include(i => i.Project)
                .ThenInclude(p => p.Client)
            .Include(i => i.InvoiceItems)
            .Where(i => i.Status == status)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> UpdateInvoiceStatusAsync(Guid invoiceId, InvoiceStatus status)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);
        if (invoice == null) return false;

        invoice.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Invoice> CreateInvoiceFromProjectAsync(Guid projectId)
    {        var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Tasks.Where(t => t.Status == Models.TaskStatus.Completed))
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
            throw new InvalidOperationException("Project not found");

        var invoice = new Invoice
        {
            ProjectId = projectId,
            InvoiceNumber = await GenerateInvoiceNumberAsync(),
            Status = InvoiceStatus.Draft,
            IssueDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(30),
            InvoiceItems = new List<InvoiceItem>()
        };        // Add completed tasks as invoice items
        foreach (var task in project.Tasks.Where(t => t.Status == Models.TaskStatus.Completed))
        {            invoice.InvoiceItems.Add(new InvoiceItem
            {
                Description = $"{task.Title} - {task.Description}",
                Quantity = 1,
                UnitPrice = task.Value
            });
        }

        // If no completed tasks, add the project as a single item
        if (!invoice.InvoiceItems.Any())
        {
            invoice.InvoiceItems.Add(new InvoiceItem
            {
                Description = $"{project.Name} - {project.Description}",
                Quantity = 1,
                UnitPrice = project.TotalValue
            });
        }

        // Calculate total amount
        invoice.TotalAmount = invoice.InvoiceItems.Sum(item => item.Total);

        return await CreateInvoiceAsync(invoice);
    }

    public async Task<string> GenerateInvoiceNumberAsync()
    {
        var year = DateTime.UtcNow.Year;
        var month = DateTime.UtcNow.Month;
        
        var lastInvoice = await _context.Invoices
            .Where(i => i.InvoiceNumber.StartsWith($"INV-{year:D4}{month:D2}-"))
            .OrderByDescending(i => i.InvoiceNumber)
            .FirstOrDefaultAsync();

        int sequenceNumber = 1;
        if (lastInvoice != null)
        {
            var lastSequence = lastInvoice.InvoiceNumber.Split('-').LastOrDefault();
            if (int.TryParse(lastSequence, out var lastNum))
            {
                sequenceNumber = lastNum + 1;
            }
        }

        return $"INV-{year:D4}{month:D2}-{sequenceNumber:D4}";
    }

    public async Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId)
    {
        var invoice = await GetInvoiceWithItemsByIdAsync(invoiceId);
        if (invoice == null)
            throw new InvalidOperationException("Invoice not found");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("INVOICE")
                    .SemiBold().FontSize(28).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        // Invoice details
                        x.Item().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"Invoice #: {invoice.InvoiceNumber}").SemiBold();
                                col.Item().Text($"Tanggal: {invoice.IssueDate:dd/MM/yyyy}");
                                col.Item().Text($"Jatuh Tempo: {invoice.DueDate:dd/MM/yyyy}");
                                col.Item().Text($"Status: {GetStatusText(invoice.Status)}");
                            });

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("Tagihan Untuk:").SemiBold();
                                col.Item().Text(invoice.Project.Client.Name);
                                if (!string.IsNullOrEmpty(invoice.Project.Client.Company))
                                    col.Item().Text(invoice.Project.Client.Company);
                                if (!string.IsNullOrEmpty(invoice.Project.Client.Email))
                                    col.Item().Text(invoice.Project.Client.Email);
                            });
                        });

                        x.Item().PaddingVertical(20).LineHorizontal(1);

                        // Project info
                        x.Item().Text($"Proyek: {invoice.Project.Name}").SemiBold().FontSize(14);
                        if (!string.IsNullOrEmpty(invoice.Project.Description))
                            x.Item().Text(invoice.Project.Description).FontSize(10);

                        x.Item().PaddingVertical(10);

                        // Invoice items table
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Deskripsi");
                                header.Cell().Element(CellStyle).Text("Qty");
                                header.Cell().Element(CellStyle).Text("Harga");
                                header.Cell().Element(CellStyle).Text("Total");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                                }
                            });

                            foreach (var item in invoice.InvoiceItems)
                            {
                                table.Cell().Element(CellStyle).Text(item.Description);
                                table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                                table.Cell().Element(CellStyle).Text($"Rp {item.UnitPrice:N0}");
                                table.Cell().Element(CellStyle).Text($"Rp {item.Total:N0}");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                }
                            }
                        });

                        x.Item().PaddingTop(10).Row(row =>
                        {
                            row.RelativeItem();
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().BorderTop(1).BorderColor(Colors.Black).PaddingTop(5);
                                col.Item().Row(r =>
                                {
                                    r.RelativeItem().Text("TOTAL:").SemiBold();
                                    r.RelativeItem().Text($"Rp {invoice.TotalAmount:N0}").SemiBold().AlignRight();
                                });
                            });
                        });

                        if (!string.IsNullOrEmpty(invoice.Notes))
                        {
                            x.Item().PaddingTop(20).Column(col =>
                            {
                                col.Item().Text("Catatan:").SemiBold();
                                col.Item().Text(invoice.Notes);
                            });
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Terima kasih atas kepercayaan Anda!");
                    });
            });
        });

        return document.GeneratePdf();
    }

    private static string GetStatusText(InvoiceStatus status)
    {
        return status switch
        {
            InvoiceStatus.Draft => "Draft",
            InvoiceStatus.Sent => "Terkirim",
            InvoiceStatus.Paid => "Lunas",
            InvoiceStatus.Overdue => "Jatuh Tempo",
            InvoiceStatus.Cancelled => "Dibatalkan",
            _ => status.ToString()
        };
    }
}
