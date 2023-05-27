using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class ProductPhoto
    {
        public long Id { get; set; }
        public byte[] ImageData { get; set; }
        public long ProductId { get; set; }
    }
}
