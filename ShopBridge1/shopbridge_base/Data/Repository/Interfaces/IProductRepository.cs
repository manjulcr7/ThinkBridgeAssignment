using Microsoft.AspNetCore.Mvc;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(int id);
        public Task<Product> PutProduct(int id, Product product);
        public Task<Product> PostProduct(Product product);
        public Task<Product> DeleteProduct(int id);
        public Task<bool> ProductExists(int id);
    }
}
