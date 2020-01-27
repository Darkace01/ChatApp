using System;

namespace ChatApp.Core
{
    public class Message{
        public int Id {get;set;}
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int ChatId {get;set;}
        public Chat Chat {get;set;}
    }
}