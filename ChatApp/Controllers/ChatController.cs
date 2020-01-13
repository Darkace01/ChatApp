using System.Threading.Tasks;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers{
    public class ChatController : Controller {
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        public async Task<IActionResult> JoinRoom(string connectionId, string roomName){
            await _chat.Groups.AddToGroupAsync(connectionId,roomName);
            return Ok();
        }

        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName){
            await _chat.Groups.RemoveFromGroupAsync(connectionId,roomName);
            return Ok();
        }
    }
}