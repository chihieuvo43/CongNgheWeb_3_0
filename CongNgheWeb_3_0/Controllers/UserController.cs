using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNgheWeb_3_0.Models;

using System.IO;
using System.Configuration;

namespace CongNgheWeb_3_0.Controllers
{
    public class UserController : Controller
    {
        E_ClassEntities db = new E_ClassEntities()
;        // GET: User
        public ActionResult Index()
        {
            return View();
        }
      
        public static string GenerateRandomPassword(int length)
        {
            string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            string allowedNumberChars = "0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }
            }
            return new string(chars);
        }

        //Get Create teacher

        public ActionResult CreateTeacher()
        {
            return View();
        }


        //Post Create teacher
        [HttpPost,ActionName("CreateTeacher")]
        public ActionResult CreateTeacherDontSendMail(string UserName, string Email, bool? Male)
        {
            string CreateRandomPassword = GenerateRandomPassword(6);
            var EmailInData = db.tbl_User.SingleOrDefault(x => x.Email == Email);
            if(EmailInData != null)
            {
                ViewBag.Status = "Email đã tồn tại. Xin hãy nhập Email khác!";
                return View();
            }
           else
            {
                tbl_User User = new tbl_User();
                if (ModelState.IsValid)
                {
                    User.Clock = false;
                    User.CreateDate = DateTime.Now;
                    User.DescriptionUser = null;
                    User.Email = Email;
                    User.Employee = true;
                    User.LoginName = Email;
                    User.NumberPhone = null;
                    User.PasswordUser = CreateRandomPassword;
                    User.UserImage = null;
                    User.UserName = UserName;
                    User.Admin = false;
                    User.PositionID = 3;
                    User.Male = false;
                    //if(Male==true)
                    //{
                    //    User.Male = false;
                    //}
                    //else
                    //{
                    //    User.Male = true;
                    //}
                    db.tbl_User.Add(User);
                    db.SaveChanges();
                    return RedirectToAction("ListTeacher","Admin");
                }

            }
          
            ViewBag.Status = "Không tạo được tài khoản";
            return View();
        }

        //Post dont send mail
        [HttpPost]
        public ActionResult CreateTeacherAndSendMail(string UserName, string Email, bool Male)
        {
            string CreateRandomPassword = GenerateRandomPassword(6);
            var EmailInData = db.tbl_User.SingleOrDefault(x => x.Email == Email);
            if (EmailInData != null)
            {
                ViewBag.Status = "Email đã tồn tại. Xin hãy nhập Email khác!";
                return View();
            }
            else
            {
                tbl_User User = new tbl_User();
                if (ModelState.IsValid)
                {
                    User.Clock = false;
                    User.CreateDate = DateTime.Now;
                    User.DescriptionUser = null;
                    User.Email = Email;
                    User.Employee = true;
                    User.LoginName = Email;
                    User.NumberPhone = null;
                    User.PasswordUser = CreateRandomPassword;
                    User.UserImage = null;
                    User.UserName = UserName;
                    User.Admin = false;
                    User.PositionID = 3;
                    if (Male == true)
                    {
                        User.Male = false;
                    }
                    else
                    {
                        User.Male = true;
                    }
                    try
                    {
                        //send mail to teacher

                        string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Admin/MailSendToTeacher.html"));

                        content.Replace("{{CustomName}}", UserName);
                        content.Replace("{{LoginName}}", Email);
                        content.Replace("{{Password}}", CreateRandomPassword);
                        var toMail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                        new MailHelper().SendMail(toMail, "Tài khoản vào EClass", content);
                    }
                    catch (Exception)
                    {
                        ViewBag.Status = "Không gửi được mail nên không tạo được tài khoản";
                        return View();
                    }


                    db.tbl_User.Add(User);
                    db.SaveChanges();
                    return RedirectToAction("ListTeacher", "Admin");
                }

            }

            ViewBag.Status = "Không tạo được tài khoản";
            return View();
        }


        //Clock and Unclock User
        public ActionResult ClockAndUnClock(int id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if(User.Clock==true)
            {
               
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                User.Clock = false;
                db.SaveChanges();
                return RedirectToAction("ListTeacher", "Admin");
            }
            else
            {
               
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                User.Clock = true;
                db.SaveChanges();
                return RedirectToAction("ListTeacher", "Admin");
            }
        }



        //Remove User

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if (User == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(User);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var User = db.tbl_User.SingleOrDefault(x => x.ID == id);
            if(User==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tbl_User.Remove(User);
            db.SaveChanges();

            return RedirectToAction("ListTeacher", "Admin");
        }


       

    }
}