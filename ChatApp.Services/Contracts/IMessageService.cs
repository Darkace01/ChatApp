using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Contracts
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
        IEnumerable<Message> GetAllUserMessages(string userId);
        Message GetMessageById(int messageId);
        Task CreateMessage(Message message);
    }
}
