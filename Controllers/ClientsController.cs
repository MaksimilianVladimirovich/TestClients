using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestClients.DTOs;
using TestClients.Interfaces;
using TestClients.Models;

namespace TestClients.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <returns>Информация о клиенте</returns>
    /// <response code="200">Клиент найден</response>
    /// <response code="404">Клиент не найден</response>
    [HttpGet("{clientId}")]
    [ProducesResponseType(typeof(ClientDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ClientDto>> GetClient(
        [Range(1, long.MaxValue, ErrorMessage = "ClientId must be greater than 0")]
            long clientId)
    {
        var client = await _clientService.GetClientAsync(clientId);
        if (client == null)
        {
            return NotFound();
        }

        return Ok(MapToDto(client));
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns>Список всех клиентов</returns>
    /// <response code="200">Успешное получение списка</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients.Select(MapToDto));
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="clientDto">Данные клиента</param>
    /// <returns>Созданный клиент</returns>
    /// <response code="201">Клиент успешно создан</response>
    /// <response code="409">Клиент с таким ID уже существует</response>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), 201)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] ClientDto clientDto)
    {
        var client = MapToEntity(clientDto);
        var result = await _clientService.CreateClientAsync(client);

        if (!result)
        {
            return Conflict("Client with this ID already exists");
        }

        return CreatedAtAction(nameof(GetClient), new { clientId = client.ClientId }, MapToDto(client));
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <param name="clientDto">Обновленные данные клиента</param>
    /// <response code="204">Клиент успешно обновлен</response>
    /// <response code="400">Несоответствие идентификаторов</response>
    /// <response code="404">Клиент не найден</response>
    [HttpPut("{clientId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateClient(
        [Range(1, long.MaxValue, ErrorMessage = "ClientId must be greater than 0")]
            long clientId,
        [FromBody] ClientDto clientDto)
    {
        if (clientId != clientDto.ClientId)
        {
            return BadRequest("Client ID mismatch");
        }

        var client = MapToEntity(clientDto);
        var result = await _clientService.UpdateClientAsync(client);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <response code="204">Клиент успешно удален</response>
    /// <response code="404">Клиент не найден</response>
    [HttpDelete("{clientId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteClient(
        [Range(1, long.MaxValue, ErrorMessage = "ClientId must be greater than 0")]
            long clientId)
    {
        var result = await _clientService.DeleteClientAsync(clientId);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Массовое добавление клиентов
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    /// POST /api/clients/batch
    /// {
    ///     "clients": [
    ///         {
    ///             "clientId": 1,
    ///             "username": "user1",
    ///             "systemId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890"
    ///         },
    ///         ... (минимум 10 объектов)
    ///     ]
    /// }
    /// </remarks>
    /// <param name="request">Запрос с массивом клиентов</param>
    /// <returns>Результат массового добавления</returns>
    /// <response code="200">Операция завершена</response>
    /// <response code="400">Неверные входные данные (менее 10 клиентов)</response>
    [HttpPost("batch")]
    [ProducesResponseType(typeof(BatchClientsResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<BatchClientsResponse>> AddClientsBatch([FromBody] BatchClientsRequest request)
    {
        if (request.Clients.Count < 10)
        {
            return BadRequest("Minimum 10 clients required");
        }

        var clients = request.Clients.Select(MapToEntity).ToList();
        var notAddedClients = await _clientService.AddClientsBatchAsync(clients);

        var response = new BatchClientsResponse
        {
            NotAddedClients = notAddedClients.Select(MapToDto),
            AddedCount = clients.Count - notAddedClients.Count(),
            NotAddedCount = notAddedClients.Count()
        };

        return Ok(response);
    }

    private ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            ClientId = client.ClientId,
            Username = client.Username,
            SystemId = client.SystemId
        };
    }

    private Client MapToEntity(ClientDto clientDto)
    {
        return new Client
        {
            ClientId = clientDto.ClientId,
            Username = clientDto.Username,
            SystemId = clientDto.SystemId
        };
    }
}