using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository.Interfaces;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<Product> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> PostProduct(Product product)
        {
            return await _productRepository.PostProduct(product);
        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            return await _productRepository.PutProduct(id,product);
        }
        public async Task<bool> ProductExists(int id)
        {
            return await _productRepository.ProductExists(id);
        }

    }
}
