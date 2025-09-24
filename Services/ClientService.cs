using TestClients.Interfaces;
using TestClients.Models;

namespace TestClients.Services;

/// <summary>
/// Сервис клиентов
/// </summary>
public class ClientService : IClientService
{
    /// <summary>
    /// Репозиторий клиентов
    /// </summary>
    private readonly IClientRepository _clientRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="clientRepository"></param>
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<Client?> GetClientAsync(long clientId)
    {
        return await _clientRepository.GetByIdAsync(clientId);
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> CreateClientAsync(Client client)
    {
        return await _clientRepository.AddAsync(client);
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> UpdateClientAsync(Client client)
    {
        return await _clientRepository.UpdateAsync(client);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteClientAsync(long clientId)
    {
        return await _clientRepository.DeleteAsync(clientId);
    }

    /// <summary>
    /// Добавить список клиентов
    /// </summary>
    /// <param name="clients"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Client>> AddClientsBatchAsync(IEnumerable<Client> clients)
    {
        return await _clientRepository.AddRangeAsync(clients);
    }
}