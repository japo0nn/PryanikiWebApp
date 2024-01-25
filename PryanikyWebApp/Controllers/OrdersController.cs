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
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("addOrder")]
        public async Task AddProduct(Guid productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                Response.StatusCode = 404;
                Response.ContentType = "application/json";
                await Response.WriteAsync($"Товара с таким ID не существует");
                return;
            }

            var newOrder = new Order
            {
                ProductId = productId,
            };

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        [HttpGet("getOrderList")]
        public async Task<IndexViewModel<OrderDto>> GetOrderList(int page = 1)
        {
            var orders = await _context.Orders.Include(x => x.Product).ToListAsync();

            var pageDto = new PageViewModel(orders.Count, page, 10);

            orders = orders
                .Skip(10 * (page - 1))
                .Take(10)
                .ToList();

            return new IndexViewModel<OrderDto>
            {
                Items = Mapper.Map<List<OrderDto>>(orders),
                PageViewModel = pageDto
            };
        }

        [HttpDelete("removeOrder")]
        public async Task RemoveOrder(Guid orderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                Response.StatusCode = 404;
                Response.ContentType = "application/json";
                await Response.WriteAsync($"Заказа с таким ID не существует");
                return;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
