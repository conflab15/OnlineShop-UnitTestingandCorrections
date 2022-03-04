using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Data;
using OnlineShop2022.Models;

namespace OnlineShop2022.Controllers
{
    public class ViewOrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<CustomUserModel> _userManager;

        public ViewOrdersController(AppDbContext context, UserManager<CustomUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var vm = new ViewOrdersViewModel();
            vm.CustomerEmail = User.Identity.Name;
            vm.Order = await _context.Orders.Where(o => o.Email == vm.CustomerEmail).ToListAsync();

            return View(vm);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (orderModel == null)
            {
                return NotFound();
            }

            orderModel.OrderLines = await _context.OrderDetails.Where(o => o.OrderId == orderModel.OrderId).Include("Product").ToListAsync();

            orderModel.OrderTotal = orderModel.OrderLines.Sum(item => item.Price * item.Amount);

            return View(orderModel);
        }
    }
}
