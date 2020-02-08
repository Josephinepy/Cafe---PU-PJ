using Newtonsoft.Json;
using Cafe.DAL;
using Cafe.Models;
using Cafe.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categories()
        {
            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult Product()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Product Table, HttpPostedFileBase file)
        {
            string Picture = null;
            if (file != null)
            {
                Picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/"), Picture);
                // file is uploaded
                file.SaveAs(path);
            }
            Table.ProductImage = file != null ? Picture : Table.ProductImage;
            Table.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Update(Table);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product Table, HttpPostedFileBase File)
        {
            string Picture = null;
            if (File != null)
            {
                Picture = System.IO.Path.GetFileName(File.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/"), Picture);
                // file is uploaded
                File.SaveAs(path);
            }
            Table.ProductImage = Picture;
            Table.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Add(Table);
            return RedirectToAction("Product");
        }
        public ActionResult ProductRemove()
        {
            return ProductDelete(0);

        }
        public ActionResult ProductDelete(int ProductId)
        {
            using (dbCafeEntities db = new dbCafeEntities())
            {
                var result = (from s in db.Products where s.ProductId == ProductId select s).FirstOrDefault();
                if (result != null)
                {
                    db.Products.Remove(result);
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("Product {0} has been deleted.", result.ProductName);
                    return RedirectToAction("Product");
                }
                else
                {
                    TempData["resultMessage"] = "Product {0} does not exist, cannot be deleted.";
                    return RedirectToAction("Product");
                }
            }
        }
        public ActionResult OrderDetail()
        {

            List<OrderDetail> result = new List<OrderDetail>();

            ViewBag.ResultMessage = TempData["ResultMessage"];

            using (dbCafeEntities db = new dbCafeEntities())
            {
                result = (from s in db.OrderDetails select s).ToList();

                return View(result);
            }
        }

        public ActionResult OrderDetailEdit(int orderId)
        {
            return View(_unitOfWork.GetRepositoryInstance<OrderDetail>().GetFirstorDefault(orderId));
        }
        [HttpPost]
        public ActionResult OrderDetailEdit(OrderDetail tbl)
        {
            _unitOfWork.GetRepositoryInstance<OrderDetail>().Update(tbl);
            return RedirectToAction("OrderDetail");
        }
        public ActionResult OrderRemove()
        {
            return OrderDelete(0);

        }
        public ActionResult OrderDelete(int OrderId)
        {
            using (dbCafeEntities db = new dbCafeEntities())
            {
                var result = (from s in db.OrderDetails where s.Id == OrderId select s).FirstOrDefault();
                if (result != null)
                {
                    db.OrderDetails.Remove(result);
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("Order {0} has been deleted.", result.Id);
                    return RedirectToAction("Order");
                }
                else
                {
                    TempData["resultMessage"] = "Order {0} does not exist, cannot be deleted.";
                    return RedirectToAction("Order");
                }
            }
        }
    }
}


