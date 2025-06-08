using Microsoft.EntityFrameworkCore;
using Aplikasi_Proyek___Tagihan_Freelancer.Data;
using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public class ClientService : IClientService
{
    private readonly ApplicationDbContext _context;

    public ClientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients
            .Include(c => c.Projects)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        return await _context.Clients
            .Include(c => c.Projects)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        client.Id = Guid.NewGuid();
        client.CreatedAt = DateTime.UtcNow;
        
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateClientAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Client>> SearchClientsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllClientsAsync();

        searchTerm = searchTerm.ToLower();
        
        return await _context.Clients
            .Include(c => c.Projects)
            .Where(c => c.Name.ToLower().Contains(searchTerm) ||
                       (c.Company != null && c.Company.ToLower().Contains(searchTerm)) ||
                       (c.Email != null && c.Email.ToLower().Contains(searchTerm)))
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}
