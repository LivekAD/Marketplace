using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class Order
    {
        public long Id { get; set; }

        public long? ProductId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public long? CartId { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
