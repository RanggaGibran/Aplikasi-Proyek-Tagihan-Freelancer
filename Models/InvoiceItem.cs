using System.ComponentModel.DataAnnotations;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public class InvoiceItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Invoice ID wajib diisi")]
    public Guid InvoiceId { get; set; }
    
    [Required(ErrorMessage = "Deskripsi item wajib diisi")]
    [StringLength(500, ErrorMessage = "Deskripsi item maksimal 500 karakter")]
    public string Description { get; set; } = string.Empty;
    
    [Range(1, int.MaxValue, ErrorMessage = "Kuantitas harus lebih besar dari 0")]
    public int Quantity { get; set; } = 1;
    
    [Range(0, double.MaxValue, ErrorMessage = "Harga satuan harus lebih besar atau sama dengan 0")]
    public decimal UnitPrice { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Total harus lebih besar atau sama dengan 0")]
    public decimal Total => Quantity * UnitPrice;
    
    // Navigation Properties
    public virtual Invoice Invoice { get; set; } = null!;
}
