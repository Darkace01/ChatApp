using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Core;
using ChatApp.Models;
using ChatApp.Data;
using ChatApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctx;
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public HomeController(ApplicationDbContext ctx, IMessageService messageService, IChatService chatService, IUserService userService)
        {
            _ctx = ctx;
            _messageService = messageService;
            _chatService = chatService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            string userId = GetUser();
            var chats = _chatService.GetAllUsersPublicChat(userId);
            return View(chats);
        }

        public IActionResult Find(){
            string userId = GetUser();
            var users = _userService.GetAllPossibleFriends(userId);
            return View(users);
        }

        public IActionResult Private(){
            
            string userId = GetUser();
                var chats = _chatService.GetAllUsersPrivateChat(userId);
            return View(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrivate(string Id)
        {
            var chat = new Chat
            {
                Type = ChatType.Private
            };
            chat.Users.Add(new ChatUser
            {
                UserId = Id
            });
            chat.Users.Add(new ChatUser
            {
                UserId = GetUser()
            });
            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();

            return RedirectToAction(nameof(Chat), new { id = chat.Id });
        }

        public async Task<IActionResult> CreatePrivateRoom(string userId){
             string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int chatId = _chatService.GetUserPreviousChat(currentUserId, userId);
            if(chatId != 00)
            {
                return RedirectToAction(nameof(Chat), new { id = chatId });
            }
            return RedirectToAction(nameof(CreatePrivate), new { Id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name){
            string userId = GetUser();
            var chat =new Chat{
                Name = name,
                Type = ChatType.Room,
            };
            await _chatService.CreateGroup(chat, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{Id}")]
        public IActionResult Chat(int Id){
           var chat = _chatService.GetChatById(Id);
            return View(chat);
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int roomId,string message){
            var messages = new Message{
                ChatId = roomId,
                Text = message,
                Name= User.Identity.Name,
                Time = DateTime.Now
            };
            await _chatService.SendMessage(messages);
            return RedirectToAction("Chat", new{Id = roomId});
        }

        [HttpGet]
        public async Task<IActionResult> JoinChat(int Id){
            var chatUser = new ChatUser{
                ChatId = Id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member
            };
            _ctx.ChatUsers.Add(chatUser);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Chat" , "Home",new{id = Id});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetUser(){
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        // private string ifChatexist(string userId){
        //     string oldChat;
        //     var newChat = _ctx.Chats.Where(x => x.Type == ChatType.Private);
        //     var oldId =  newChat.FirstOrDefault(x => x.Users.Where(y => y.UserId.Contains(userId)));

        //     return oldChat;
        // }
    }
}
