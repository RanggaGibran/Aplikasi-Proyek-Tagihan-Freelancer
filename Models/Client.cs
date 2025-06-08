using System.ComponentModel.DataAnnotations;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public class Client
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Nama klien wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama klien maksimal 100 karakter")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(100, ErrorMessage = "Nama perusahaan maksimal 100 karakter")]
    public string? Company { get; set; }
    
    [EmailAddress(ErrorMessage = "Format email tidak valid")]
    [StringLength(100, ErrorMessage = "Email maksimal 100 karakter")]
    public string? Email { get; set; }
    
    [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
    [StringLength(20, ErrorMessage = "Nomor telepon maksimal 20 karakter")]
    public string? PhoneNumber { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
