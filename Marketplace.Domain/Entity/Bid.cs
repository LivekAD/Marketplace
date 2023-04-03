using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class Bid
    {
        public long Id { get; set; }

        public long AuctionId { get; set; }

        public decimal lastBid { get; set; }

        public string ProductName { get; set; }

        public string? BidUserName { get; set; }

        public decimal? BidAmount { get; set; }

    }
}
