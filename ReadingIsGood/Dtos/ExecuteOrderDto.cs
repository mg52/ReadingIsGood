using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class ExecuteOrderDto
    {
        public ExecuteOrderDto()
        {
            OrderIds = new List<Guid>();
        }

        public List<Guid> OrderIds { get; set; }
    }
}
