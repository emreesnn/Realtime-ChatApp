using ChatApp.Dtos;
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
        public async Task<IActionResult> GetConversationWithUser(string currentUserName, string targetUserName)
        {
            List<Message> messages =  await _messageService.GetConversationAsync(currentUserName, targetUserName);

            var messageHistory = messages.Select(m => new MessageDto
            {
                SenderName = m.SenderName,
                ReceiverName = m.ReceiverName,
                Content = m.Content,
                Timestamp = m.Timestamp
            }).ToList();

            return Ok(messageHistory);

        }
    }

}
