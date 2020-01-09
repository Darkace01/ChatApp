using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;
using ChatApp.Data;

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
