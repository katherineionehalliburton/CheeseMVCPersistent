using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }


        public IActionResult Index()
        {
            var Menus = context.Menus.ToList();

            return View(Menus);
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

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }


        public IActionResult ViewMenu(int id)
        {
            try
            {
                Menu menus = context.Menus.Single(m => m.ID == id);

                List<CheeseMenu> items = context
                    .CheeseMenus
                    .Include(item => item.Cheese)
                    .Where(cm => cm.MenuID == id)
                    .ToList();

                ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
                {
                    Menus = menus,
                    Items = items
                };

                return View(viewMenuViewModel);
            }
            catch
            {
                return Redirect("/");
            }
        }

        public IActionResult AddItem(int id)
        {
            Menu menus = context.Menus.Single(m => m.ID == id);

            List<Cheese> cheeses = context.Cheeses.ToList();

            return View(new AddMenuItemViewModel(menus, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            var cheeseID = addMenuItemViewModel.CheeseID;
            var menuID = addMenuItemViewModel.MenuID;

            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu cheeseMenu = new CheeseMenu
                    {
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menus = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();
                }
                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }
            return View(addMenuItemViewModel);
        }

    }
}
