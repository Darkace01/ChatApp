using ChatApp.Core;
using ChatApp.Data.Contracts;
using ChatApp.Data.Implementations;
using ChatApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatApp.Services.Implementations
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

        public async Task CreateGroup(Chat chat,string userId){
            chat.Users.Add(new ChatUser{
                UserId = userId,
                Role = UserRole.Admin
            });

            _uow.ChatRepo.Add(chat);
            await _uow.Save();
        }

        public async Task SendMessage(Message message){
            _uow.MessageRepo.Add(message);
            await _uow.Save();
        }
        public async Task UpdateChat(Chat chat)
        {
            // implement this later

            _uow.ChatRepo.Update(chat);
            await _uow.Save();
        }

        public async Task JoinChat(ChatUser chat){
            _uow.ChatUserRepo.Add(chat);
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

        public IEnumerable<Chat> GetAllUsersPrivateChat(string userId){
            return _uow.ChatRepo.GetAllChatWithRelationships().Where(x => x.Users.Any(y => y.UserId == userId && y.Chat.Type.ToString().ToLower() == "private")).ToList();
        }

        public IEnumerable<Chat> GetAllUsersPublicChat(string userId){
            return _uow.ChatRepo.GetAllChatWithRelationships().Where(x => !x.Users.Any(y => y.UserId == userId && y.Chat.Type.ToString().ToLower() == "publlic")).ToList();
        }
        

        public IEnumerable<Chat> GetAllChats()
        {
            return _uow.ChatRepo.GetAll();
        }
        public Chat GetChatById(int chatId)
        {
            return _uow.ChatRepo.GetAllChatWithRelationships().Where(x => x.Id == chatId).FirstOrDefault();
        }

        public Chat GetChatByName(string chatName)
        {
            return _uow.ChatRepo.GetAllChatWithRelationships().Where(p => string.Compare(p.Name,chatName,true) == 0).FirstOrDefault();
        }

        public bool CheckIfChatAlreadyExistForUser(string userId, int chatId)
        {
            var chat = GetAllUsersPrivateChat(userId).Where(x => x.Users.Any(y => y.ChatId == chatId));

            if(chat != null){
                return true;
            }
            return false;

        }

        public int GetUserPreviousChat(string userId, string preUserId)
        {
            var chat = GetAllUsersPrivateChat(userId).Where(x => x.Users.Any(y => y.User.Id == preUserId)).FirstOrDefault();
            if(chat != null)
            {
                return chat.Id;
            }
            else
            {
                return 00;
            }
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
