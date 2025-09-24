namespace TestClients.DTOs;

/// <summary>
/// Ответ на массовое добавление клиентов
/// </summary>
public class BatchClientsResponse
{
    /// <summary>
    /// Клиенты, которые не были добавлены (дубликаты)
    /// </summary>
    public IEnumerable<ClientDto> NotAddedClients { get; set; } = new List<ClientDto>();

    /// <summary>
    /// Количество успешно добавленных клиентов
    /// </summary>
    /// <example>5</example>
    public int AddedCount { get; set; }

    /// <summary>
    /// Количество не добавленных клиентов
    /// </summary>
    /// <example>5</example>
    public int NotAddedCount { get; set; }
}