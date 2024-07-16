using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrishnaDairyDotNetApi.Models
{
    public class CartModel
    {
        public long ProductID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }
    }
}
