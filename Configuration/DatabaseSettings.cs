namespace TestClients.Configuration;

/// <summary>
/// Настройки по базе данных
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// Строка подключения
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}