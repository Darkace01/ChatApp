using System;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Core;
using ChatApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat, IChatService chatService, IMessageService messageService)
        {
            _chat = chat;
            _chatService = chatService;
            _messageService = messageService;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId){
            await _chat.Groups.AddToGroupAsync(connectionId,roomId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId){
            await _chat.Groups.RemoveFromGroupAsync(connectionId,roomId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string message,int roomId, [FromServices] ApplicationDbContext ctx){

            var messages = new Message{
                ChatId = roomId,
                Text = message,
                Name= User.Identity.Name,
                Time = DateTime.Now
            };
            await _messageService.CreateMessage(messages);
            
            await _chat.Clients.Group(roomId.ToString()).SendAsync("RecieveMessage", new {
                Text = messages.Text,
                Name = messages.Name,
                Time = messages.Time.ToString("dd/MM/yyyy hh:mm:ss")
            });
            return Ok();
        }
    }
}