using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data.Implementations;
using ChatApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly UnitOfWork _uow;
        public MessageService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }
        public async Task CreateMessage(Message message)
        {
            if (message == null)
                // implement this later

                _uow.MessageRepo.Add(message);
            await _uow.Save();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _uow.MessageRepo.GetAll();
        }

        public IEnumerable<Message> GetAllUserMessages(string userId)
        {
            return _uow.MessageRepo.GetAll().Where(p => p.Chat.Users.Any(y => y.UserId == userId));
        }

        public Message GetMessageById(int messageId)
        {
            return _uow.MessageRepo.Find(p => p.Id == messageId).FirstOrDefault();
        }
        


    }
}
