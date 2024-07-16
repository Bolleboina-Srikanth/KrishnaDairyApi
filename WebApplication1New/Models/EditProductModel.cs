using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrishnaDairyDotNetApi.Models
{
    public class EditProductModel
    {
        public long productId { get; set; }

        public string productName { get; set; }

        public decimal originalPrice { get; set; }

        public decimal discountPrice { get; set; }

        public IFormFile image { get; set; }
    }
}
