using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1New.Models
{
    public class ProductModal
    {
        public string productName { get; set; }

        public decimal originalPrice { get; set; }

        public decimal discountPrice { get; set; }

        public decimal quantity { get; set; }

        public string category { get; set; }

        public IFormFile image { get; set; }
    }
}
