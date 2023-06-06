using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Repositories;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Enum;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Chat;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.User;
using Marketplace.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Implementations
{
    public class ChatMessageService : IChatMessageService
    {

        #region Include Data Base

        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<ChatMessage> _chatMessageRepository;

        public ChatMessageService(IBaseRepository<Product> productRepository, IBaseRepository<User> userRepository, IBaseRepository<ChatMessage> chatMessageRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _chatMessageRepository = chatMessageRepository;
        }

		#endregion


		#region Create Chat
		public async Task<IBaseResponse<ChatMessage>> CreateChat(string productId, string user1, string user2)
        {
            try
            {
                var user1Id = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == user1);
                var user2Id = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == user2);

                var chat = new ChatMessage
                {
                    ProductId = productId,
                    User1Id = user1Id.Id.ToString(),
                    User1Name = user1,
                    User2Id = user2Id.Id.ToString(),
                    User2Name = user2,
                    Message = null,
                    Timestamp = DateTime.Now,
                    GroupName = GetGroupName(productId, user1Id.Id.ToString(), user2Id.Id.ToString()),
                };

                await _chatMessageRepository.Create(chat);

                return new BaseResponse<ChatMessage>()
                {
                    StatusCode = StatusCode.OK,
                    Data = chat
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatMessage>()
                {
                    Description = $"[GetOrCreateChat] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		#endregion

		#region Send Message
		public async Task<IBaseResponse<ChatMessage>> SendMessage(string productId, string sender, string message, string GroupName)
		{
			try
            {
				var groupId = SplitString(GroupName);
				var userId = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == groupId[2]);
                var resUserId = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == groupId[1]);
				var senderId = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == sender);
                
                if(senderId.Id != userId.Id)
                {
					var chat = new ChatMessage
					{
						ProductId = productId,
						User1Id = senderId.Id.ToString(),
						User1Name = sender,
						User2Id = userId.Id.ToString(),
						User2Name = userId.Name,
						Message = message,
						Timestamp = DateTime.Now,
						GroupName = GroupName,
					};
					await _chatMessageRepository.Create(chat);

					return new BaseResponse<ChatMessage>()
					{
						StatusCode = StatusCode.OK,
						Data = chat
					};
				}
                else
                {
					var chat = new ChatMessage
					{
						ProductId = productId,
						User1Id = senderId.Id.ToString(),
						User1Name = sender,
						User2Id = resUserId.Id.ToString(),
						User2Name = resUserId.Name,
						Message = message,
						Timestamp = DateTime.Now,
						GroupName = GroupName,
					};
					await _chatMessageRepository.Create(chat);

					return new BaseResponse<ChatMessage>()
					{
						StatusCode = StatusCode.OK,
						Data = chat
					};
				}
            }
            catch (Exception ex)
            {
                return new BaseResponse<ChatMessage>()
                {
                    Description = $"[GetOrCreateChat] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

		#endregion

		#region Get Message

		public async Task<IBaseResponse<List<ChatMessage>>> GetMessages(string productId, string user1, string user2)
        {
            try
            {
                var user1Id = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == user1);
                var user2Id = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == user2);

                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id.ToString() == productId);
                //var chatsToProduct = _chatMessageRepository.GetAll().Where(c => c.ProductId == productId).OrderBy(c => c.Timestamp);
                var chats = _chatMessageRepository.GetAll().Where(c => c.ProductId == productId &&
                        (c.User1Id == user1Id.Id.ToString() || c.User1Id == user2Id.Id.ToString()) &&
                        (c.User2Id == user1Id.Id.ToString() || c.User2Id == user2Id.Id.ToString())).OrderBy(c => c.Timestamp).ToList();

                product.ChatMessages = chats;
                await _productRepository.Update(product);

                if (!chats.Any())
                {
                    return new BaseResponse<List<ChatMessage>>()
                    {
                        Description = "Знайдено 0 елементів",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<ChatMessage>>()
                {
                    Data = chats,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<ChatMessage>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
		#endregion

		public string GetGroupName(string productId, string user1Id, string user2Id)
        {
            return $"{productId}-{user1Id}-{user2Id}";
        }

        static long[] SplitString(string input)
        {
            string[] parts = input.Split('-');
            long[] numbers = new long[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                numbers[i] = long.Parse(parts[i]);
            }

            return numbers;
        }

        #region Get Chats

        public async Task<IBaseResponse<List<ChatMessage>>> GetChats(string user)
		{
			try
			{
				var userId = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == user);
                var chats = _chatMessageRepository.GetAll().Where(c => c.User1Id == userId.Id.ToString() || c.User2Id == userId.Id.ToString()).ToList();
                //var product = await _productRepository.GetAll().FirstOrDefaultAsync();

				return new BaseResponse<List<ChatMessage>>()
				{
					Data = chats,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<ChatMessage>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Внутренняя ошибка: {ex.Message}"
				};
			}
		}

		#endregion
	}

	


}
