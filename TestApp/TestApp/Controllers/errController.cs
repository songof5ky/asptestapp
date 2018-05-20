using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Controllers
{
    public class errController : Controller
    {

        private TestContext db;
        public errController(TestContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        { return View(); }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(errModel model)
        {

            if (ModelState.IsValid)
            {
                err error = await db.Errors.FirstOrDefaultAsync(u => u.id == model.id);
                if (error == null)
                {

                    db.Errors.Add(new err { criticalityId = model.criticalityId, statusId  = 1, urgencyId = model.urgencyId, f_descript = model.f_descript, s_descript = model.s_descript, date = DateTime.Now, user_id = User.Identity.Name, });

                    await db.SaveChangesAsync();
                    err error1 = await db.Errors.LastOrDefaultAsync(p => p.id > 0);
                    
                    db.ErHstr.Add(new ErrorHistory { date = DateTime.Now, act = "Ввод", user_id = User.Identity.Name, errId = error1.id });
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Такая ошибка уже есть в базе");
            }
            return View(model);
        }
        [Authorize]


        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                err error = await db.Errors.Include(p=>p.Histories).Include(p=>p.status).Include(p=>p.urgency).Include(p=>p.criticality).FirstOrDefaultAsync(p => p.id == id);
                var hstr = db.ErHstr.Where(p => p.errId == id);
                
                editClass ed = new editClass
                {
                    Err = error,
                    ErHs = hstr.ToArray()
                };

                ErrorHistory[] e = hstr.ToArray();

                 
                return View(error);
            }
            return NotFound();
        }
        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id != null)
            {
                err error = await db.Errors.FirstOrDefaultAsync(p => p.id == id);
                var hstr = db.ErHstr.Where(p => p.errId == id);
                editClass ed = new editClass
                {
                    Err = error,
                    ErHs = hstr.ToArray()
                };
              
                return View(ed);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(editClass ed)
        {
            
            
             err err1 = await db.Errors.FirstOrDefaultAsync(p => p.id == ed.Err.id);

          

            string a = Request.Form["status"].ToString();
          
            
            

                if (err1.statusId != 4)
                {



                    if (a == "Открытая" )
                    {
                        Models.Status st = await db.Statuses.FirstOrDefaultAsync(p => p.id == 2);
                        err1.status = st;
                        
                    }
                    if (a == "Решенная")
                    {
                        Models.Status st = await db.Statuses.FirstOrDefaultAsync(p => p.id == 3);
                        err1.status = st;

                       
                    }
                    if (a == "Закрытая")
                    {
                       Models.Status st = await db.Statuses.FirstOrDefaultAsync(p => p.id == 4);
                       err1.status = st;
                       

                    }
                    ErrorHistory e = new ErrorHistory();
                    if (a == "Открытая")
                        e.act = "Открытие";
                    if (a == "Решенная")
                        e.act = "Решение";
                    if (a == "Закрытая")
                        e.act = "Закрытие";
                    ed.Err.user_id = User.Identity.Name;

                    e.errId = ed.Err.id;
                    

                    if (ed.buf != null && err1.status != null)
                        e.comnt = ed.buf;


                    e.date = DateTime.Now;
                    e.user_id = ed.Err.user_id;

                    db.ErHstr.Add(e);
                    db.Errors.Update(err1);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            

            return RedirectToAction("Index", "Home");
            
        }




    }
}