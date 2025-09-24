using System.ComponentModel.DataAnnotations;

namespace TestClients.DTOs;

/// <summary>
/// Запрос на массовое добавление клиентов
/// </summary>
public class BatchClientsRequest
{
    /// <summary>
    /// Список клиентов для добавления (минимум 10)
    /// </summary>
    [Required]
    [MinLength(10, ErrorMessage = "Минимум десять клиентов")]
    public List<ClientDto> Clients { get; set; } = new();
}