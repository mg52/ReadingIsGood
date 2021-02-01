using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class OrderItemFilterDto
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
