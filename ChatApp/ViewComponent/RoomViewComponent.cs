using System.Linq;
using System.Security.Claims;
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
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _ctx.ChatUsers
            .Where(x => x.UserId == userId)
            .ToList();
            return View(chats);
        }
    }
}