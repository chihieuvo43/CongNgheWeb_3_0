using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

namespace CongNgheWeb_3_0.Controllers
{
    public class CategoryController : Controller
    {
        E_ClassEntities db = new E_ClassEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.tbl_Category.ToList());
        }
        
        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_Category category)
        {
            try
            {
                var categoryInData = db.tbl_Category.SingleOrDefault(x => x.CategoryName == category.CategoryName);
                if(categoryInData!=null)
                {
                    ViewBag.Status = "Danh mục này đã tồn tại";
                    return View();
                }
                if(ModelState.IsValid)
                {
                    db.tbl_Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index","Category");
                }
                ViewBag.Status = "Không thêm được danh mục";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Không thêm được danh mục"+ex;
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var Category = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if(Category==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                 return View(Category);
            }
           
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Category category)
        {  
              
                    if(ModelState.IsValid)
                    {
                        var CategoryInData = db.tbl_Category.SingleOrDefault(x=>x.CategoryName==category.CategoryName && x.ID!=category.ID);
                        if (CategoryInData != null)
                        {
                            ViewBag.Status = "Danh mục " + category.CategoryName + " đã tồn tại";
                            return View(category);
                        }
                        db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ViewBag.Status = "Không sửa được danh mục";
                        return View(category);
                    }
                  
                
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.Status = "Không sửa được danh mục"+ex;
            //    return View(category);
            //}
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var CategoryInData = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if(CategoryInData==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(CategoryInData);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var CategoryInData = db.tbl_Category.SingleOrDefault(x => x.ID == id);
            if (CategoryInData == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.tbl_Category.Remove(CategoryInData);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
        }
        //Partil view for list Category

        public PartialViewResult PartilViewListCategory()
        {
            var ListCategory = db.tbl_Category.ToList();
            return PartialView(ListCategory);
        }
    
    }
}
