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
                // throw new ProductNotFoundException("chat cannot be null!");

            // if (!ValidateChatDetails(product))
            //     throw new InvalidProductDataException();

            _uow.ChatRepo.Add(chat);
            await _uow.Save();
        }

        public async Task UpdateChat(Chat chat)
        {
            // if (chat == null)
            //     throw new ProductNotFoundException("product cannot be null!");

            // if (!ValidateCreateProductDetails(product))
            //     throw new InvalidProductDataException();

            _uow.ChatRepo.Update(chat);
            await _uow.Save();
        }

        public async Task DeleteChat(Chat chat)
        {
            // if (product == null)
            //     throw new ProductNotFoundException("product cannot be null!");

            
            _uow.ChatRepo.Update(chat);
            await _uow.Save();
        }


        public IEnumerable<Chat> GetAllUserChat(string userId)
        {
            return _uow.ChatRepo.GetAll().Where(p => p.userId == userId);
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
            return _uow.ProductRepo.Find(p => string.Compare(p.Name, name, true) == 0).Where(c => !c.IsDeleted).FirstOrDefault();
        }
        public enum ChatType{
        Room,
        Private
        }

        public string CheckIfChatAlreadyExistForUser(string userId, string chatUserId)
        {
            var existingChat = _uow.ChatRepo.Find().Where(c => c.ChatType == ChatType.Private);
            var user = existingChat.Find().Where(e => e.User.Id == userId);
            if (existedProduct == null)
                return false;

            return true ;

        }

        private bool ValidateCreateProductDetails(Product product)
        {
            bool result = true;

            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrWhiteSpace(product.Name)
                || product.Name.Length<2 || product.Name.Length>30)
                return false;

            if (string.IsNullOrEmpty(product.IconUrl) || string.IsNullOrWhiteSpace(product.IconUrl))
                return false;

            Company company = _uow.CompanyRepo.Get(product.CompanyId);
            if (company == null)
                return false;

            if (!char.IsLetterOrDigit(product.Name[0]))
                return false;


            if (product.Pieces < 1)
                return false;
            

            return result;
        }

    }
}
