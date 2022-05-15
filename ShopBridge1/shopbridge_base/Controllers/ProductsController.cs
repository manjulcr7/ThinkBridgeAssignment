using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService _productService, ILogger<ProductsController> logger)
        {
            this.productService = _productService;
            _logger = logger;
        }
       
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await productService.GetProducts();
                return Ok(products);
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Error occured while getting all products:{e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while getting all products");
            }
         
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Invalid Parameters");
                var productExists = await productService.ProductExists(id);
                if (!productExists) return StatusCode(StatusCodes.Status400BadRequest, "Product with specified id doesn't exist");
                var product = await productService.GetProduct(id);
                return product;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error occured while getting product by id({id}):{e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while getting product by id");
            }
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            try
            {
                if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Invalid Parameters");
                if (string.IsNullOrEmpty(product.Name)) return StatusCode(StatusCodes.Status400BadRequest, "Product Name is Required");
                var productExists = await productService.ProductExists(id);
                if(!productExists) return StatusCode(StatusCodes.Status400BadRequest, "Product with specified id doesn't exist");
                var updatedProduct = await productService.PutProduct(id, product);
                return Ok(updatedProduct);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error occured while updating product:{e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while updating product");
            }

        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Name)) return StatusCode(StatusCodes.Status400BadRequest, "Product Name is Required");
                var newProduct = await productService.PostProduct(product);
                return newProduct;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error occured while adding new product:{e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while adding new product");
            }

        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Invalid Parameters");

                var productExists = await productService.ProductExists(id);
                if (!productExists) return StatusCode(StatusCodes.Status400BadRequest, "Product with specified id doesn't exist");

                var product = await productService.DeleteProduct(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error occured while deleting product:{e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while deleting product");
            }

        }

    }
}
