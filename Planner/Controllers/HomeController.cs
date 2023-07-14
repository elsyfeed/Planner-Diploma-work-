using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planner.Models;
using System.Diagnostics;
using System.Security.Claims;
using Task = Planner.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Planner.Data;
using Microsoft.AspNetCore.Authorization;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private readonly ApplicationDbContext _context;
        [Authorize]
        public async Task<IActionResult> Index(Task task, string filterName)
        {
            //IQueryable<Task> tasksQuery = _context.Tasks;

            if (User.Identity.IsAuthenticated)
            {

                var email = User.FindFirstValue(ClaimTypes.Email);
                var tasksQuery = _context.Tasks.Where(t => t.Email == email);

                if (!string.IsNullOrEmpty(filterName))
                {
                    tasksQuery = tasksQuery.Where(t => t.Name.Contains(filterName));
                }

                var tasks = await tasksQuery.ToListAsync();

                ViewBag.FilterName = filterName;
                ViewBag.AllNames = await _context.Tasks.Where(t => t.Email == email).Select(t => t.Name).Distinct().ToListAsync();

                return View(tasks);
            }
            return _context.Tasks != null ?
                          View(await _context.Tasks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tasks' is null.");


        }



        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}