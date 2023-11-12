﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;
using websitebanhang.Models;

namespace websitebanhang.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        WebSiteBhEntities ojbWebSiteBhEntities = new WebSiteBhEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                //Gán dữ liệu cho bảng Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                ojbWebSiteBhEntities.Orders.Add(objOrder);
                //Lưu thông tin dữ liệu vào bảng order
                ojbWebSiteBhEntities.SaveChanges();

                //Lưu thông tin dữ liệu vào bảng OrderDetail.
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                ojbWebSiteBhEntities.OrderDetails.AddRange(lstOrderDetail);
                ojbWebSiteBhEntities.SaveChanges();
            }
            return View();
        }
    }
}