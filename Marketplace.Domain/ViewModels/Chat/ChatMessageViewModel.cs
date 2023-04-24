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

        public int ProductId { get; set; }

        public string User1Id { get; set; }

        public string User2Id { get; set; }

        public IEnumerable<ChatMessage> Chats { get; set; }

    }
}
