using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }


        public IActionResult Index()
        {
            var Menus = context.Menu.ToList();

            return View("/Index");
        }

       
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menu.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }    
    }
}
