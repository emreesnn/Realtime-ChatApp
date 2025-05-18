using ChatApp.Data.DTO;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public ChatController(IMessageService messageService) {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            //TODO: Set a limit - try to optimize - get for an sender or group
            List<Message> messages =  await _messageService.GetAllMessagesAsync();

            var messageHistory = messages.Select(m => new MessageHistory
            {
                Sender = m.Sender,
                Content = m.Content,
                Timestamp = m.Timestamp
            }).ToList();

            return Ok(messageHistory);

        }
    }

}
