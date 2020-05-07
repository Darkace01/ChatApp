using ChatApp.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Contracts
{
    public interface IChatService
    {
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Chat> GetAllUserChat(string userId);
        Chat GetChatById(int chatId);
        Chat GetChatByName(string chatName);
        Task CreateChat(Chat chat);
        bool CheckIfChatAlreadyExistForUser(string userId, int chatId);
        IEnumerable<Chat> GetAllUsersPrivateChat(string userId);
        Task UpdateChat(Chat chat);
        Task DeleteChat(Chat chat);
        int GetUserPreviousChat(string userId, string preUserId);
    }
}
