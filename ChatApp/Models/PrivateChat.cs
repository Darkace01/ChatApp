using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class PrivateChat{
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserId { get; set; }
    }
}