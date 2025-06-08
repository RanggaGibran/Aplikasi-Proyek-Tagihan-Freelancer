using System.ComponentModel.DataAnnotations;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public class ProjectTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
      [Required(ErrorMessage = "Project ID wajib diisi")]
    public Guid ProjectId { get; set; }
    
    [Required(ErrorMessage = "Judul tugas wajib diisi")]
    [StringLength(200, ErrorMessage = "Judul tugas maksimal 200 karakter")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Deskripsi tugas wajib diisi")]
    [StringLength(500, ErrorMessage = "Deskripsi tugas maksimal 500 karakter")]
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
    
    [Range(0, double.MaxValue, ErrorMessage = "Nilai tugas harus lebih besar atau sama dengan 0")]
    public decimal Value { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Estimasi jam harus lebih besar atau sama dengan 0")]
    public decimal? EstimatedHours { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation Properties
    public virtual Project Project { get; set; } = null!;
}
