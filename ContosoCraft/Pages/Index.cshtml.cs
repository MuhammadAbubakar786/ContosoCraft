using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCraft.Model;
using ContosoCraft.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCraft.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ProductService _ProductService;
        public IEnumerable<Product> products;
        public IndexModel(ILogger<IndexModel> logger , ProductService productService)
        {
            _logger = logger;
            _ProductService = productService;
        }

        public void OnGet()
        {
            products = _ProductService.GetProducts();
        }
    }
}
