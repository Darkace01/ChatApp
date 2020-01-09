using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;
using ChatApp.Data;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        public async Task<IActionResult> CreateRoom(string name){
            _ctx.Chats.Add(new Chat{
                Name = name,
                Type = ChatType.Room,
            });
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
        public async Task<IActionResult> CreateMessage(int chatId,string message){
            var Message = new Message{
                ChatId = chatId,
                Text = message,
                Name= "Default",
                Time = DateTime.Now
            };
            _ctx.Messages.Add(Message);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new{Id = chatId});
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
