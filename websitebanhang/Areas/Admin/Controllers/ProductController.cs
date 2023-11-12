﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;
using static websitebanhang.Common;

namespace websitebanhang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebSiteBhEntities objWebSiteBhEntities = new WebSiteBhEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //Lấy ds sản phẩm theo từ khóa tìm kiếm
                lstProduct = objWebSiteBhEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstProduct = objWebSiteBhEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //Số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            try
            {
                if (objProduct.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                    objProduct.Avatar = fileName;
                    objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    objWebSiteBhEntities.Products.Add(objProduct);
                    objWebSiteBhEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        void LoadData()
        {
            Common objCommon = new Common();
            //Lấy dữ liệu danh mục dưới database
            var lstCat = objWebSiteBhEntities.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            //Convert sang select list dạng value, text
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //Lấy dữ liệu danh mục dưới database
            var lstBrand = objWebSiteBhEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //Convert sang select list dạng value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //Convert sang select list dạng value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objWebSiteBhEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebSiteBhEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebSiteBhEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebSiteBhEntities.Products.Remove(objProduct);
            objWebSiteBhEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWebSiteBhEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddmmss")) + extension;
                objProduct.Avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objWebSiteBhEntities.Entry(objProduct).State = EntityState.Modified;
            objWebSiteBhEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}