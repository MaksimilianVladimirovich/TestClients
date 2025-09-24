using LiteDB;
using Microsoft.Extensions.Options;
using TestClients.Configuration;
using TestClients.Interfaces;
using TestClients.Models;

namespace TestClients.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly LiteDatabase _database;
    private readonly ILiteCollection<Client> _collection;

    public ClientRepository(IOptions<DatabaseSettings> settings)
    {
        _database = new LiteDatabase(settings.Value.ConnectionString);
        _collection = _database.GetCollection<Client>("clients");
        _collection.EnsureIndex(x => x.ClientId, unique: true);
    }

    /// <summary>
    /// Добавить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(Client client)
    {
        return await Task.Run(() =>
        {
            try
            {
                _collection.Insert(client);
                return true;
            }
            catch (LiteException ex) when (ex.ErrorCode == LiteException.INDEX_DUPLICATE_KEY)
            {
                return false;
            }
        });
    }

    /// <summary>
    /// Добавить список клиентов
    /// </summary>
    /// <param name="clients"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Client>> AddRangeAsync(IEnumerable<Client> clients)
    {
        var notAddedClients = new List<Client>();

        foreach (var client in clients)
        {
            var exists = await ExistsAsync(client.ClientId);
            if (!exists)
            {
                var added = await AddAsync(client);
                if (!added)
                {
                    notAddedClients.Add(client);
                }
            }
            else
            {
                notAddedClients.Add(client);
            }
        }

        return notAddedClients;
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(long clientId)
    {
        return await Task.Run(() => _collection.DeleteMany(x => x.ClientId == clientId) > 0);
    }

    /// <summary>
    /// Существует ли клиент
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<bool> ExistsAsync(long clientId)
    {
        return await Task.Run(() => _collection.Exists(x => x.ClientId == clientId));
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await Task.Run(() => _collection.FindAll());
    }

    /// <summary>
    /// Получить клиента
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public async Task<Client?> GetByIdAsync(long clientId)
    {
        return await Task.Run(() => _collection.FindOne(x => x.ClientId == clientId));
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(Client client)
    {
        return await Task.Run(() => _collection.Update(client));
    }

    public void Dispose()
    {
        _database?.Dispose();
    }
}
