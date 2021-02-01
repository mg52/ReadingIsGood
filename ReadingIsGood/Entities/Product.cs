using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int BookCode { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
