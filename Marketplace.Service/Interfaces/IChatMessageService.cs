﻿using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Chat;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IChatMessageService
    {
        //Task<IBaseResponse<ChatMessage>> AddMessage(string chatId, string senderId, string receiverId, string message);

        Task<IBaseResponse<ChatMessage>> CreateChat(string productId, string user1, string user2);

        Task<IBaseResponse<ChatMessage>> SendMessage(string productId, string sender, string message, string GroupName);

        Task<IBaseResponse<List<ChatMessage>>> GetMessages(string productId, string user1, string user2);

		Task<IBaseResponse<List<ChatMessage>>> GetChats(string user);

		string GetGroupName(string productId, string user1Id, string user2Id);

    }
}
