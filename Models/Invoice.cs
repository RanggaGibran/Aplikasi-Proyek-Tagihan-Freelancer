using System.ComponentModel.DataAnnotations;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Project ID wajib diisi")]
    public Guid ProjectId { get; set; }
    
    [Required(ErrorMessage = "Nomor invoice wajib diisi")]
    [StringLength(50, ErrorMessage = "Nomor invoice maksimal 50 karakter")]
    public string InvoiceNumber { get; set; } = string.Empty;
    
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    
    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(30);
    
    [Range(0, double.MaxValue, ErrorMessage = "Total amount harus lebih besar atau sama dengan 0")]
    public decimal TotalAmount { get; set; }
    
    [StringLength(1000, ErrorMessage = "Catatan maksimal 1000 karakter")]
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public virtual Project Project { get; set; } = null!;
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
