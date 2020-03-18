using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data.Implementations;
using ChatApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SavvyLaundry.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly UnitOfWork _uow;
        public ChatService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public async Task CreateChat(Chat chat)
        {
            if (chat == null)
                // implement this later

            _uow.ChatRepo.Add(chat);
            await _uow.Save();
        }

        public async Task UpdateChat(Chat chat)
        {
            // implement this later

            _uow.ChatRepo.Update(chat);
            await _uow.Save();
        }

        public async Task DeleteChat(Chat chat)
        {
            // implement this later

            
            _uow.ChatRepo.Update(chat);
            await _uow.Save();
        }


        public IEnumerable<Chat> GetAllUserChat(string userId)
        {
            return _uow.ChatRepo.GetAll().Where(p => p.Users.Any(y => y.UserId == userId));
            
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return _uow.ChatRepo.GetAll();
        }
        public Chat GetChatById(int chatId)
        {
            return _uow.ChatRepo.Find(p => p.Id == chatId).FirstOrDefault();
        }

        public Chat GetChatByName(string chatName)
        {
            return _uow.ChatRepo.Find(p => string.Compare(p.Name, chatName, true) == 0).FirstOrDefault();
        }
        public enum ChatType{
        Room,
        Private
        }

        public string CheckIfChatAlreadyExistForUser(string userId, string chatUserId)
        {
            //implement this later

            return "" ;

        }

        private bool ValidateCreateChatDetails(Chat chat)
        {
            bool result = true;

            if (string.IsNullOrEmpty(chat.Name) || string.IsNullOrWhiteSpace(chat.Name)
                || chat.Name.Length<2 || chat.Name.Length>30)
                return false;

            Chat chats = _uow.ChatRepo.Get(chat.Id);
            if (chats == null)
                return false;
            

            return result;
        }

    }
}
