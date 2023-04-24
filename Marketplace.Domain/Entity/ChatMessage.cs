using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User1Name { get; set; }
        public string User2Id { get; set; }
        public string User2Name { get; set; }
        public string ProductId { get; set; }
        public string? Message { get; set; }
        public string GroupName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
