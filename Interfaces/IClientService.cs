using TestClients.Models;

namespace TestClients.Interfaces;

/// <summary>
/// Сервис клиентов
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<Client?> GetClientAsync(long clientId);

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Client>> GetAllClientsAsync();

    /// <summary>
    /// Создать клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    Task<bool> CreateClientAsync(Client client);

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    Task<bool> UpdateClientAsync(Client client);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<bool> DeleteClientAsync(long clientId);

    /// <summary>
    /// Добавить список клиентов
    /// </summary>
    /// <param name="clients"></param>
    /// <returns></returns>
    Task<IEnumerable<Client>> AddClientsBatchAsync(IEnumerable<Client> clients);
}
