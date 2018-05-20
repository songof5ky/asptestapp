using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private TestContext db;
        public HomeController(TestContext context)
        {
            db = context;
        }
 
        [Authorize]
        public async Task<IActionResult> Index(SortState sortOrder = SortState.idAsc)
        {
            IQueryable<err> errors = db.Errors.ToList().AsQueryable();
            ViewData["idSort"] = sortOrder == SortState.idAsc ? SortState.idDesc : SortState.idAsc;
            ViewData["dateSort"] = sortOrder == SortState.dateAsc ? SortState.dateDesc : SortState.dateAsc;
            ViewData["usernameSort"] = sortOrder == SortState.usernameAsc ? SortState.usernameDesc : SortState.usernameAsc;

            switch (sortOrder)
            {
                case SortState.idDesc:
                    errors = errors.OrderByDescending(s => s.id);
                    break;
                case SortState.idAsc:
                    errors = errors.OrderBy(s => s.id);
                    break;
                case SortState.dateDesc:
                    errors = errors.OrderByDescending(s => s.date);
                    break;
                case SortState.dateAsc:
                    errors = errors.OrderBy(s => s.date);
                    break;
                case SortState.usernameDesc:
                    errors = errors.OrderByDescending(s => s.user_id);
                    break;
                default:
                    errors = errors.OrderBy(s => s.user_id);
                    break;
            }
            return View(  errors.AsNoTracking().ToList());
            
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [Authorize]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
