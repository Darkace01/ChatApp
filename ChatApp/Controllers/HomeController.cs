﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;
using ChatApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _ctx;
        public HomeController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            var chats = _ctx.Chats
            .Include(x => x.Users)
            .Where(x => !x.Users
            .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
            .ToList();
            return View(chats);
        }

        public async Task<IActionResult> CreateRoom(string name){
            var chat =new Chat{
                Name = name,
                Type = ChatType.Room,
            };
            chat.Users.Add(new ChatUser{
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });
            _ctx.Chats.Add(chat);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{Id}")]
        public IActionResult Chat(int Id){
            var chat = _ctx.Chats
            .Include(x => x.Messages)
            .FirstOrDefault(x => x.Id == Id);
            return View(chat);
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId,string messageText){
            var message = new Message{
                ChatId = chatId,
                Text = messageText,
                Name= User.Identity.Name,
                Time = DateTime.Now
            };
            _ctx.Messages.Add(message);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new{Id = chatId});
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
    }
}
