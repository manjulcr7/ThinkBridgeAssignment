using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository.Interfaces;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly Shopbridge_Context _context;
        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(Shopbridge_Context context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(s => s.Product_Id.Equals(id));
            if (product == null) throw new Exception("Product doesn't exist");
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(s => s.Product_Id.Equals(id));
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Product.ToListAsync();
            return products;
        }

        public async Task<Product> PostProduct(Product product)
        {
            // setting Product_Id to 0 as IDENTITY INSERT is OFF
            product.Product_Id = 0;
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            var oldProduct = await _context.Product.FirstOrDefaultAsync(s => s.Product_Id.Equals(id));
            if (oldProduct == null) throw new Exception("Product doesn't exist");
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            oldProduct.Image = product.Image;
            var contextChanged = _context.Product.Attach(oldProduct);
            contextChanged.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> ProductExists(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(s => s.Product_Id.Equals(id));
            if (product == null) return false;
            return true;
        }
    }
}
