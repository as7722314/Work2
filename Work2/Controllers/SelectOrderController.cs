using System;
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
        public ActionResult SelectList(Index arg)
        {   ///取得訂單資料
            OrderService orderService = new OrderService();
            ViewBag.Orderdata = orderService.GetOrderCondition(arg);
            return View();
        }
        [HttpGet]
        public ActionResult Del(int orderid)
        {
            OrderService orderService = new OrderService();
            orderService.Del(orderid);
            return RedirectToAction("Index", "SelectOrder");
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
        public ActionResult InsertOrder(Order arg)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                OrderService orderService = new OrderService();
                orderService.InsertOrder(arg);
                return RedirectToAction("Index", "SelectOrder");
            }
            else
            {
                EmployeeService employeeService = new EmployeeService();
                ViewBag.employeelist = employeeService.GetEnameList();
                ShipperService shipperService = new ShipperService();
                ViewBag.shipperlist = shipperService.GetSnameList();
                CustomerService customerservice = new CustomerService();
                ViewBag.customerlist = customerservice.GetCustomerList();
                return View();
            }
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
            ///ViewBag.customerlist = shippersitems;
            return View();


        }
        [HttpPost]
        public ActionResult Update(Order arg)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                OrderService orderService = new OrderService();
                orderService.Update(arg);
                return RedirectToAction("Index", "SelectOrder");
            }
            else
            {   ///獲得訂單資料
                OrderService orderService = new OrderService();

                ///準備員工下拉式選單           
                EmployeeService employeeservice = new EmployeeService();
                List<SelectListItem> employeeitems = employeeservice.GetEnameList();
                ViewBag.employeelist = employeeitems;
                ///準備物流下拉式選單
                ShipperService shippersservice = new ShipperService();
                List<SelectListItem> shippersitems = shippersservice.GetSnameList();
                ViewBag.shipperslist = shippersitems;
                ///準備員工下拉式選單
                CustomerService customerservice = new CustomerService();
                List<SelectListItem> customeritems = customerservice.GetCustomerList();
                ViewBag.customerlist = customeritems;
                return View();
            }
        }
        [HttpGet]
        public JsonResult InsertProduct()
        {
            OrderService orderService = new OrderService();
            List<SelectListItem> result = orderService.GetOrderDetailList();
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPrice(string arg)
        {
            OrderService orderService = new OrderService();
            string result = orderService.GetUnitPrice(arg);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}