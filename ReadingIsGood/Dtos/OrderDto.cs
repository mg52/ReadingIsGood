using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public bool Status { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
