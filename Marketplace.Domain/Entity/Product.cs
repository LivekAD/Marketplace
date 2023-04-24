using Marketplace.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }

        public Category Category { get; set; }

        public SubCategory SubCategory { get; set; }

        public string? OwnerName { get; set; }

        public byte[]? Photo { get; set; }

        public string? isAuction { get; set; }

        public DateTime? EndingAuction { get; set; }

        public virtual List<Bid>? Bids { get; set; }

        public List<ChatMessage>? ChatMessages { get; set; }

    }
}
