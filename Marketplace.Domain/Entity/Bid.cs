using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class Bid
    {
        public int Id { get; set; }

        public decimal BidAmount { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
