using System.ComponentModel.DataAnnotations;

namespace TestClients.DTOs;

/// <summary>
/// DTO для клиента
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    /// <example>12345</example>
    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "ClientId должен быть больше нуля")]
    public long ClientId { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    /// <example>john_doe</example>
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Имя должно содержать как минимум 1 символ и не больше пятидесяти")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Системный идентификатор
    /// </summary>
    /// <example>a1b2c3d4-e5f6-7890-abcd-ef1234567890</example>
    [Required]
    public Guid SystemId { get; set; }
}