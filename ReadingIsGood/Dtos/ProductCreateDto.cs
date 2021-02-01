﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReadingIsGood.Dtos
{
    public class ProductCreateDto
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public int BookCode { get; set; }
    }
}
