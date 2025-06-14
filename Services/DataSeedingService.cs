using Aplikasi_Proyek___Tagihan_Freelancer.Data;
using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public class DataSeedingService
{
    private readonly ApplicationDbContext _context;

    public DataSeedingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        // Check if data already exists
        if (_context.Clients.Any())
        {
            return; // Data already seeded
        }
        
        // Database is empty, but we're leaving it for manual data entry
        // No sample data will be created
        await Task.CompletedTask;
    }
}