using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Repositories;
using Marketplace.Domain.Entity;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Marketplace.Hubs
{
    [Authorize]
    public class ConnectedHub : Hub
    {
        private readonly IChatMessageService _messageService;
        private readonly IUserService _userService;

        public ConnectedHub(IChatMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

		public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

		public async Task SendMessage(int productId, string user1, string user2, string message)
        {
            var user1Id = _userService.GetUser(user1);
            var user2Id = _userService.GetUser(user2);
            var chat = await _messageService.GetOrCreateChat(productId.ToString(), user1, user2, message);
            var chatId = _messageService.GetGroupName(productId.ToString(), user1Id.Result.Data.Id.ToString(), user2Id.Result.Data.Id.ToString());

            await Clients.Group(chatId).SendAsync("ReceiveMessage", chat);
        }

    }
}
