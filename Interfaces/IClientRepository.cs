using TestClients.Models;

namespace TestClients.Interfaces;

/// <summary>
/// Действия с клиентом
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Получить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<Client?> GetByIdAsync(long clientId);

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Добавить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    Task<bool> AddAsync(Client client);

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(Client client);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long clientId);

    /// <summary>
    /// Клиент существует
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(long clientId);

    /// <summary>
    /// Добавить список клиентов
    /// </summary>
    /// <param name="clients"></param>
    /// <returns></returns>
    Task<IEnumerable<Client>> AddRangeAsync(IEnumerable<Client> clients);
}
