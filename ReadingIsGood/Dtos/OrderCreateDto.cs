using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class OrderCreateDto
    {
        [JsonIgnore]
        public Guid CustomerId { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
