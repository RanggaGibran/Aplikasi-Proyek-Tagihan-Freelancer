using Aplikasi_Proyek___Tagihan_Freelancer.Models;

namespace Aplikasi_Proyek___Tagihan_Freelancer.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(Guid id);
    Task<Client> CreateClientAsync(Client client);
    Task<Client> UpdateClientAsync(Client client);
    Task<bool> DeleteClientAsync(Guid id);
    Task<IEnumerable<Client>> SearchClientsAsync(string searchTerm);
}
