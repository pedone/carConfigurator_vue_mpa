﻿using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class OrderListController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OrderListController));

        [Route("orders", Name = Constants.Routes.OrderList)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var orderCount = context.Orders.Count();
                var pageCount = (int)Math.Ceiling((double)orderCount / Constants.PageItemsCount);
                return View(new OrderListPageViewModel
                {
                    PageCount = pageCount,
                    Orders = context.Orders.Take(Constants.PageItemsCount).ToList()
                    .Select(cur => new OrderViewModel(cur, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = cur.Guid })))
                    .ToList()
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete(int id)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var toDelete = context.Orders.Find(id);
                if (toDelete != null)
                {
                    toDelete.Configuration.Accessories.Clear();
                    context.Orders.Remove(toDelete);
                    context.SaveChanges();
                }
            }

            return string.Empty;
        }

        [HttpGet]
        public ActionResult LoadPage(int pageIndex)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var orders = context.Orders.ToList().Skip(Constants.PageItemsCount * pageIndex).Take(Constants.PageItemsCount).ToList()
                .Select(cur => new OrderViewModel(cur, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = cur.Guid })))
                .ToList();
                return Json(orders, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
