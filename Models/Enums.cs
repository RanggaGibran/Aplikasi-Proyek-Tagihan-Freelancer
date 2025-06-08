namespace Aplikasi_Proyek___Tagihan_Freelancer.Models;

public enum ProjectStatus
{
    Baru = 0,
    Aktif = 1,
    OnHold = 2,
    Tertunda = 2, // Alias for OnHold
    Selesai = 3,
    Dibatalkan = 4
}

public enum TaskStatus
{
    ToDo = 0,
    InProgress = 1,
    Completed = 2
}

public enum InvoiceStatus
{
    Draft = 0,
    Sent = 1,
    Paid = 2,
    Overdue = 3,
    Cancelled = 4
}
