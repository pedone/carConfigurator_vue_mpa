﻿using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class AccessoriesController : Controller
    {
        [Route("configuration/models/model-{id}/accessories", Name = Constants.Routes.Accessories)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var settings = context.Accessories.ToList()
                    .Select(cur => new AccessoryViewModel(cur))
                    .OrderBy(cur => cur.Price)
                    .ToList();
                var itemSelectionSource = new ViewModelSelection<AccessoryViewModel>(settings);
                return View(itemSelectionSource);
            }
        }
    }
}