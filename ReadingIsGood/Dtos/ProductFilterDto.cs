using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class ProductFilterDto
    {
        public Guid? Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int? BookCode { get; set; }
    }
}
