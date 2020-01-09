using System.Linq;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent 
    {
        private ApplicationDbContext _ctx;
        public RoomViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IViewComponentResult Invoke(){
            var chats = _ctx.Chats.ToList();
            return View(chats);
        }
    }
}