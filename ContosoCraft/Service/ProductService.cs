using ContosoCraft.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContosoCraft.Service
{
    public class ProductService
    {

        public ProductService(IWebHostEnvironment webHostEnvironment)
        {

            hostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment hostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(hostEnvironment.WebRootPath, "Data", "Product.json"); }
        }
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();
            var query = products.First(t => t.Id == productId);
            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }), products);
            };
        }

    }
}

