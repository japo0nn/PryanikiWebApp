using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryanikyWebApp.Context;
using PryanikyWebApp.Data;
using PryanikyWebApp.Dto;
using PryanikyWebApp.Model;

namespace PryanikyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("addProduct")]
        public async Task AddProduct(ProductDto productDto)
        {
            var newProduct = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        [HttpGet("getProductList")]
        public async Task<IndexViewModel<ProductDto>> GetProductList(int page = 1)
        {
            var products = await _context.Products.ToListAsync();

            var pageDto = new PageViewModel(products.Count, page, 10);

            products = products
                .Skip(10 * (page - 1))
                .Take(10)
                .ToList();

            return new IndexViewModel<ProductDto>
            {
                Items = Mapper.Map<List<ProductDto>>(products),
                PageViewModel = pageDto
            };
        }

        [HttpDelete("removeProduct")]
        public async Task RemoveProduct(Guid productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                Response.StatusCode = 404;
                Response.ContentType = "application/json";
                await Response.WriteAsync($"Продукт с таким ID не существует");
                return;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
