using System;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller {
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName){
            await _chat.Groups.AddToGroupAsync(connectionId,roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName){
            await _chat.Groups.RemoveFromGroupAsync(connectionId,roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string message,string roomName, int chatId, [FromServices] ApplicationDbContext ctx){

            var messages = new Message{
                ChatId = chatId,
                Text = message,
                Name= User.Identity.Name,
                Time = DateTime.Now
            };
            ctx.Messages.Add(messages);
            await ctx.SaveChangesAsync();
            
            await _chat.Clients.Group(roomName).SendAsync("RecieveMessage", new {
                Text = messages.Text,
                Name = messages.Name,
                Time = messages.Time.ToString("dd/MM/yyyy hh:mm:ss")
            });
            return Ok();
        }
    }
}