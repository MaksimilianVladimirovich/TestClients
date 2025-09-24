namespace TestClients.Models;

/// <summary>
/// Клиент
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public long ClientId { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор системы
    /// </summary>
    public Guid SystemId { get; set; }
}
