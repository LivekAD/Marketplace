using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.ViewModels.Chat
{
    public class ChatMessageViewModel
    {
        public long Id { get; set; }

        public List<ChatMessage> Chats { get; set; }

        public List<Marketplace.Domain.Entity.Product> Products { get; set; }

    }
}
