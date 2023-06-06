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

		/*public async Task JoinChat(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }*/

		public async Task SendMessage(string productId, string GroupName, string user1, string user2, string message)
        {
            var chat = await _messageService.SendMessage(productId, user1, message, GroupName);
            //var chatId = _messageService.GetGroupName(productId.ToString(), user1Id.Result.Data.Id.ToString(), user2Id.Result.Data.Id.ToString());
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
            await Clients.Group(GroupName).SendAsync("ReceiveMessage", message);
        }

    }
}
