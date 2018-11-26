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
    public class ModelController : Controller
    {
        [Route("configuration/models", Name = Constants.Routes.ModelOverview)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(context.CarModels.ToList().Select(cur => new CarModelViewModel(cur)).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetCarModel(int id)
        {
            if (!Request.IsAjaxRequest())
                throw new InvalidOperationException("This action must be called with ajax");

            using (var context = new CarConfiguratorEntityContext())
            {
                var selectedCarModel = context.CarModels.FirstOrDefault(cur => cur.Id == id);
                if (selectedCarModel == null)
                    throw new ArgumentException($"Car model with id {id} was not found in database");

                SessionData.ActiveConfiguration.Reset();

                //set model
                SessionData.ActiveConfiguration.CarModel = new CarModelViewModel(selectedCarModel);

                //set default engine settings
                var cheapestSettings = selectedCarModel.EngineSettings
                    .OrderBy(cur => cur.Price)
                    .First();
                SessionData.ActiveConfiguration.EngineSettingsId = cheapestSettings.Id;

                //set default paint
                SessionData.ActiveConfiguration.PaintId = context.Paints.First(cur => cur.IsDefault == true).Id;

                //set default rims
                SessionData.ActiveConfiguration.RimId = context.Rims.First(cur => cur.IsDefault == true).Id;

                return new EmptyResult();
            }
        }
    }
}