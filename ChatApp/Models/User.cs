using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class User : IdentityUser{
        public string ImgUrl {get;set;}
        public ICollection<ChatUser> Chats { get; set; }
    }
}