using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class StockCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
