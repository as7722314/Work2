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
        {
            EmployeeService employeeservice = new EmployeeService();
            List<SelectListItem> namelist = employeeservice.GetNameList();
            ViewBag.namelist = namelist;
            ShipperService shipperservice = new ShipperService();
            List<SelectListItem> shipperlist = shipperservice.GetShipperList();
            ViewBag.shipperlist = shipperlist;
            return View();
        }

        [HttpPost]
        public ActionResult SelectList(Index arg)
        {
            OrderService orderservice = new OrderService();
            List<Order> orders = orderservice.GetOrderCondition(arg);
            ViewBag.orderlist = orders;
            return View();
        }

        [HttpGet]
        public ActionResult InsertOrder()
        {
            EmployeeService employeeservice = new EmployeeService();
            List<SelectListItem> namelist = employeeservice.GetNameList();
            ViewBag.namelist = namelist;
            ShipperService shippersservice = new ShipperService();
            List<SelectListItem> shipperlist = shippersservice.GetShipperList();
            ViewBag.shipperlist = shipperlist;
            CustomerService customerservice = new CustomerService();
            List<SelectListItem> customerlist = customerservice.GetCustomerList();
            ViewBag.customerlist = customerlist;
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
                EmployeeService employeeservice = new EmployeeService();
                List<SelectListItem> namelist = employeeservice.GetNameList();
                ViewBag.employeelist = namelist;
                ShipperService shippersservice = new ShipperService();
                List<SelectListItem> shipperlist = shippersservice.GetShipperList();
                ViewBag.shipperlist = shipperlist;
                CustomerService customerservice = new CustomerService();
                List<SelectListItem> customerlist = customerservice.GetCustomerList();
                ViewBag.customerlist = customerlist;
                return View("InsertOrder");
            }
        }
        [HttpGet]
        public ActionResult Del(int orderid)
        {
            OrderService orderService = new OrderService();
            orderService.Del(orderid);
            return RedirectToAction("Index", "SelectOrder");
        }
        [HttpGet]
        public ActionResult Update(int orderid)
        {
            OrderService orderService = new OrderService();
            ViewBag.updateorder = orderService.GetOrders(orderid);
            EmployeeService employeeservice = new EmployeeService();
            List<SelectListItem> namelist = employeeservice.GetNameList();
            ViewBag.employeelist = namelist;
            ShipperService shippersservice = new ShipperService();
            List<SelectListItem> shipperslist = shippersservice.GetShipperList();
            ViewBag.shipperslist = shipperslist;
            CustomerService customerservice = new CustomerService();
            List<SelectListItem> customerlist = customerservice.GetCustomerList();
            ViewBag.customerlist = customerlist;
            return View();
        }
    }
}