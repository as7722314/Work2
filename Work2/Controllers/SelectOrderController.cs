﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work2.Models;
using Work2.Models.Services;


namespace Work2.Controllers
{
    public class SelectOrderController : Controller
    {
        // GET: SelectOrder
        [HttpGet]
        public ActionResult Index()
        {   ///員工、物流公司下拉是選單
            EmployeeService employeeService = new EmployeeService();
            ViewBag.employeelist = employeeService.GetEnameList();
            ShipperService shipperService = new ShipperService();
            ViewBag.shipperlist = shipperService.GetSnameList();
            return View();
        }
        [HttpPost]
        public JsonResult SelectList(Index arg)
        {   ///取得訂單資料
            OrderService orderService = new OrderService();
            ///return this.Json(orderService.GetOrderCondition(arg));          
            return Json(orderService.GetOrderCondition(arg), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Delete(int orderid)
        {
            OrderService orderService = new OrderService();
            orderService.Del(orderid);
            return RedirectToAction("Index", "SelectOrder");

        }
        [HttpPost]
        public JsonResult Del(int orderid)
        {
            OrderService orderService = new OrderService();
            string messagebox = orderService.Del(orderid);
            return this.Json(messagebox);

        }
        [HttpGet]
        public ActionResult InsertOrder()
        {
            ///準備員工、物流、顧客的下拉式選單
            EmployeeService employeeService = new EmployeeService();
            ViewBag.employeelist = employeeService.GetEnameList();
            ShipperService shipperService = new ShipperService();
            ViewBag.shipperlist = shipperService.GetSnameList();
            CustomerService customerservice = new CustomerService();
            ViewBag.customerlist = customerservice.GetCustomerList();
            return View();
        }
        [HttpPost]
        public JsonResult InsertOrder(Order arg)
        {
            string message = "";
            if (arg.OrderDetail != null)
            {
                OrderService orderService = new OrderService();
                message = orderService.InsertOrder(arg);
            }
            else
            {
                message = "未填寫商品明細";
            }
            return this.Json(message);
        }
        /// <summary>
        /// 更新資料頁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(int orderid)
        {
            ///獲得訂單資料
            OrderService orderService = new OrderService();
            Order orderdata = orderService.GetOrders(orderid);
            ///取得產品資料
            List<SelectListItem> productitems = orderService.GetOrderDetailList();
            ViewBag.productitems = productitems;
            ///準備員工下拉式選單           
            EmployeeService employeeservice = new EmployeeService();
            List<SelectListItem> employeeitems = employeeservice.GetEnameList();
            ViewBag.employeelist = employeeitems;

            ///準備物流下拉式選單
            ShipperService shipperservice = new ShipperService();
            List<SelectListItem> shippersitems = shipperservice.GetSnameList();
            ViewBag.shipperslist = shippersitems;

            ///準備員工下拉式選單
            CustomerService customerservice = new CustomerService();
            List<SelectListItem> customeritems = customerservice.GetCustomerList();
            ViewBag.customerlist = shippersitems;
            return View(orderdata);


        }
        [HttpPost]
        public JsonResult Update(Order arg)        {

            OrderService orderService = new OrderService();
            string message = orderService.Update(arg);
            return this.Json(message);

        }
        [HttpGet]
        public JsonResult InsertProduct()
        {
            OrderService orderService = new OrderService();
            List<SelectListItem> result = orderService.GetOrderDetailList();///取得產品ID與價格
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPrice(string arg)
        {
            OrderService orderService = new OrderService();
            string result = orderService.GetUnitPrice(arg);///取得Price
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}