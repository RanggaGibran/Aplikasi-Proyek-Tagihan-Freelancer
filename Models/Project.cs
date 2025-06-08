using System.ComponentModel.DataAnnotations;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Client ID wajib diisi")]
    public Guid ClientId { get; set; }
    
    [Required(ErrorMessage = "Nama proyek wajib diisi")]
    [StringLength(200, ErrorMessage = "Nama proyek maksimal 200 karakter")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(1000, ErrorMessage = "Deskripsi maksimal 1000 karakter")]
    public string? Description { get; set; }
    
    public ProjectStatus Status { get; set; } = ProjectStatus.Baru;
      [Range(0, double.MaxValue, ErrorMessage = "Total nilai harus lebih besar atau sama dengan 0")]
    public decimal TotalValue { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? Deadline { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Budget harus lebih besar atau sama dengan 0")]
    public decimal? Budget { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Tarif per jam harus lebih besar atau sama dengan 0")]
    public decimal? HourlyRate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public virtual Client Client { get; set; } = null!;
    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
