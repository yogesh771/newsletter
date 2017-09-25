using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewLetter.Models;
using System.Dynamic;
using System.Data.Entity.Validation;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;
using System.Web.Security;
using System.Drawing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace NewLetter.Controllers
{
    public class NewLetterController : Controller
    {
        oriondbEntities db = null;
        public NewLetterController()
        {
            db = new oriondbEntities();
        }

        // GET: NewsLetter

        public ActionResult Introduction(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);

                if (qenid != 0)
                {
                    var qenList = db.qendidateLists.Where(e => e.qenID == qenid).FirstOrDefault();
                    if (qenList != null)
                    {
                        Session["LoginID"] = qenid;
                        ViewBag.qenid = qenid;
                        if (qenList.qenName != null && qenList.qenName != "")
                        {
                            ViewBag.candidateName = qenList.qenName;
                        }
                        else
                        {
                            ViewBag.candidateName = "Unknown";
                        }
                        return View();
                    }
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                app_error_log oapp_error_log = new app_error_log();
                
                oapp_error_log.user_id =Convert.ToInt32( qenid);
                oapp_error_log.error_message= ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                TempData["msg"] = "Error : " + ex.Message.ToString();
                return RedirectToAction("Error");
            }
        }
        public ActionResult PersonalInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);

                if (qenid == (long)Session["LoginID"])
                {
                    qendidateList model = null;
                    model = db.qendidateLists.Where(ex => ex.qenID == qenid).FirstOrDefault();
                    if (model != null)
                    {
                        model.city1 = Find_city(model.qenCityID);
                        ViewBag.genderID = new SelectList(db.genderLists, "genderID", "genderName", model.genderID);

                    }
                    return View(model);
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }


        public string Find_city(int cityID)
        {

            var city = db.cities.Where(e => e.cityID == cityID).Select(e => new { e.city1 }).FirstOrDefault();
            return city.city1;
        }

        [HttpPost]
        public JsonResult select_city(string Prefix)
        {

            var cityName = db.cities.Where(m => m.city1.StartsWith(Prefix)).Select(x => new { x.cityID, x.city1 });
            return Json(cityName, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePersonalInfo(qendidateList model, HttpPostedFileBase files)
        {
           try
            {

                String fileName = "";
                if (files != null)
                {
                    fileName = Guid.NewGuid() + "_" + Path.GetFileName(files.FileName);
                    var path = Path.Combine(Server.MapPath("~/Logo/"), fileName);
                    files.SaveAs(path);
                    fileName = "http://newsletter.qendidate.com/Logo/" + fileName;
                }

                qendidateList data = null;
                if (ModelState.IsValid)
                {
                    data = db.qendidateLists.Where(ex => ex.qenID == model.qenID).FirstOrDefault();
                    if (data != null)
                    {
                        data.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        data.qenEmail = model.qenEmail;
                        data.qenCityID = checkCityExist(model.city1.ToString());
                        data.qenAddress = model.qenAddress;
                        data.qenName = model.qenName;
                        data.qenNationality = model.qenNationality;
                        data.qenPassport = model.qenPassport;
                        data.qenLinkdInUrl = model.qenLinkdInUrl;
                        data.qenPhone = model.qenPhone;
                        data.genderID = model.genderID;
                        data.qenImage = fileName;
                        db.SaveChanges();
                    }
                    else
                    {
                        model.qenImage = fileName;
                        model.qenCityID = checkCityExist(model.city1);
                        db.qendidateLists.Add(model);
                        db.SaveChanges();

                    }
                }
                return RedirectToAction("AcademicInfo", new { qenid = model.qenID });
            }
           catch(Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(model.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }

        }


        public async Task<ActionResult> send(candidateRegistration candidateReg)
        {
            qendidateList qenlist = new qendidateList();


            qenlist.qenName = candidateReg.candidateName;
            qenlist.qenEmail = candidateReg.Email;
            qenlist.password = candidateReg.password;
            qenlist.qenID = candidateReg.candidateID;

            var apiKey = Environment.GetEnvironmentVariable("companyregistrationemail", EnvironmentVariableTarget.User);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("info@qendidate.com", "Qendidate");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(qenlist.qenEmail, qenlist.qenName);
            var plainTextContent = "";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";

            var htmlContent = "<h1>The following email was sent to you by " + qenlist.qenEmail + ".</h1><br />";
            htmlContent += "New Password <b>" + qenlist.password + "</b><br />";
            htmlContent += "<a href = 'http://localhost:51126/Account/login?qenid=" + qenlist.qenID + "'> Click Here</a>";



            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            return View();
        }

        private int checkCityExist(string city)
        {
            var result = db.cities.Where(ex => ex.city1 == city).Select(x => new { x.cityID }).SingleOrDefault();
            if (result == null)
            {
                city oskill = new city();
                oskill.city1 = city;
                oskill.stateID = 2;
                oskill.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oskill.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                oskill.isActive = true;
                oskill.isDelete = false;
                db.cities.Add(oskill);
                db.SaveChanges();
                return oskill.cityID;
            }
            else
            {
                return result.cityID;
            }
        }

      
        public ActionResult AcademicInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    ViewBag.qenid = qenid;
                    return View();
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public ActionResult highSchool(long qenid)
        {
            try
            {
                if (qenid == (long)Session["LoginID"])
                {
                    qenSecondary oqenSecondary = null;
                    oqenSecondary = db.qenSecondaries.Where(e => e.qenID == qenid).FirstOrDefault();
                    if (oqenSecondary != null)
                    {
                        return PartialView("_partialHighSchool", oqenSecondary);
                    }
                    else
                    {
                        oqenSecondary = new qenSecondary();
                        oqenSecondary.qenID = qenid;
                        return PartialView("_partialHighSchool");
                    }
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult highSchool(qenSecondary oqenSecondary)
        {
          try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qenSecondaries.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qenSecondaries.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qenSecondaries.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.schoolName = oqenSecondary.schoolName;

                        oqenSecondary1.secondaryBoard = oqenSecondary.secondaryBoard;
                        oqenSecondary1.secondaryPassYear = oqenSecondary.secondaryPassYear;
                        oqenSecondary1.secondaryPercentage = oqenSecondary.secondaryPercentage;
                        oqenSecondary1.secondarySubjects = oqenSecondary.secondarySubjects;

                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("AcademicInfo", new { qenid = oqenSecondary.qenID });
            }
           catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(oqenSecondary.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult higherSchool(long qenid)
        {
            try { 
            if (qenid == (long)Session["LoginID"])
            {
                qenHigherSecondary oqenSecondary = null;
                oqenSecondary = db.qenHigherSecondaries.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialHigherSchool", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qenHigherSecondary();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialHigherSchool");
                }
            }
            else
            {
                TempData["msg"] = "Invalid user to update record";
                return RedirectToAction("Error");
            }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult higherSchool(qenHigherSecondary oqenSecondary)
        {
           try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qenHigherSecondaries.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qenHigherSecondaries.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qenHigherSecondaries.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.hSecondaryBoard = oqenSecondary.hSecondaryBoard;

                        oqenSecondary1.hSecondaryPassYear = oqenSecondary.hSecondaryPassYear;
                        oqenSecondary1.hSecondaryPercentage = oqenSecondary.hSecondaryPercentage;
                        oqenSecondary1.hSecondarySubjects = oqenSecondary.hSecondarySubjects;
                        oqenSecondary1.schoolName = oqenSecondary.schoolName;

                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("AcademicInfo", new { qenid = oqenSecondary.qenID });
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(oqenSecondary.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }

        }



        [HttpGet]
        public ActionResult Graduation(long qenid)
        {
            try { 
            if (qenid == (long)Session["LoginID"])
            {
                qendidateGraduation oqenSecondary = null;
                oqenSecondary = db.qendidateGraduations.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialGraduation", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qendidateGraduation();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialGraduation");
                }
            }
            else
            {
                TempData["msg"] = "Invalid user to update record";
                return RedirectToAction("Error");
            }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult Graduation(qendidateGraduation oqenSecondary)
        {
            try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qendidateGraduations.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qendidateGraduations.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qendidateGraduations.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.gradPercentage = oqenSecondary.gradPercentage;

                        oqenSecondary1.subjects = oqenSecondary.subjects;
                        oqenSecondary1.YearPassing = oqenSecondary.YearPassing;
                        oqenSecondary1.collegeName = oqenSecondary.courseName;
                        oqenSecondary1.collegeUniversity = oqenSecondary.collegeUniversity;
                        oqenSecondary1.courseField = oqenSecondary.courseField;
                        oqenSecondary1.courseName = oqenSecondary.courseName;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;

                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("AcademicInfo", new { qenid = oqenSecondary.qenID });
            }

            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(oqenSecondary.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }

        }
    [HttpGet]
        public ActionResult PostGraduation(long qenid)
        {
            try { 
            if (qenid == (long)Session["LoginID"])
            {
                qendidatePGraduation oqenSecondary = null;
                oqenSecondary = db.qendidatePGraduations.Where(e => e.qenID == qenid).FirstOrDefault();
                if (oqenSecondary != null)
                {
                    return PartialView("_partialPostGraduation", oqenSecondary);
                }
                else
                {
                    oqenSecondary = new qendidatePGraduation();
                    oqenSecondary.qenID = qenid;
                    return PartialView("_partialPostGraduation");
                }
            }
            else
            {
                TempData["msg"] = "Invalid user to update record";
                return RedirectToAction("Error");
            }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult PostGraduation(qendidatePGraduation oqenSecondary)
        {
           try
            {
                oqenSecondary.dataIsCreated = BaseUtil.GetCurrentDateTime();
                oqenSecondary.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                if (ModelState.IsValid)
                {
                    if (!db.qendidatePGraduations.Any(e => e.qenID == oqenSecondary.qenID))
                    {
                        db.qendidatePGraduations.Add(oqenSecondary);
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                    else
                    {
                        var oqenSecondary1 = db.qendidatePGraduations.Where(e => e.qenID == oqenSecondary.qenID).FirstOrDefault();
                        oqenSecondary1.pGradPercentage = oqenSecondary.pGradPercentage;

                        oqenSecondary1.subjects = oqenSecondary.subjects;
                        oqenSecondary1.YearPassing = oqenSecondary.YearPassing;
                        oqenSecondary1.collegeName = oqenSecondary.courseName;
                        oqenSecondary1.collegeUniversity = oqenSecondary.collegeUniversity;
                        oqenSecondary1.courseField = oqenSecondary.courseField;
                        oqenSecondary1.courseName = oqenSecondary.courseName;
                        oqenSecondary1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        db.Entry(oqenSecondary1).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Success";
                    }
                }
                else
                {
                    TempData["msg"] = "fail";
                }
                return RedirectToAction("AcademicInfo", new { qenid = oqenSecondary.qenID });
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(oqenSecondary.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }



        public ActionResult PhdInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    qendidatePHD model = null;

                    model = db.qendidatePHDs.Where(ex => ex.qenID == qenid).FirstOrDefault();
                    return View(model);
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }

        }
        public ActionResult EmplomentInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    EmployerModel model = null;
                    List<qenEmpDetail> emp = null;
                    if (qenid != 0)
                    {
                        emp = db.qenEmpDetails.Where(ex => ex.qenID == qenid).ToList();
                        if (emp.Count > 0 && emp != null)
                        {
                            model = new EmployerModel();
                            model.employers = emp;
                            model.qenid = qenid;
                        }
                        else
                        {
                            model = new EmployerModel();
                            model.qenid = qenid;
                        }
                    }
                    else
                    {
                        model = new EmployerModel();
                        model.qenid = qenid;
                    }
                    ViewBag.qenID = qenid;
                    return View(model);
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        public ActionResult RefrenceInfo(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    qenrefvalidation model = null;
                    List<qenReference> reflist = db.qenReferences.Where(ex => ex.qenID == qenid).OrderBy(ex => ex.qenRefID).ToList();
                    if (reflist != null && reflist.Count() > 1)
                    {
                        model = new qenrefvalidation();
                        model.qenRefName1 = reflist[0].qenRefName;
                        model.qenRefPhone1 = reflist[0].qenRefPhone;
                        model.qenRefPosition1 = reflist[0].qenRefPosition;
                        model.qenRefEmail1 = reflist[0].qenRefEmail;
                        model.qenRefCompany1 = reflist[0].qenRefCompany;
                        model.qenRefName2 = reflist[1].qenRefName;
                        model.qenRefPhone2 = reflist[1].qenRefPhone;
                        model.qenRefPosition2 = reflist[1].qenRefPosition;
                        model.qenRefEmail2 = reflist[1].qenRefEmail;
                        model.qenRefCompany2 = reflist[1].qenRefCompany;
                       
                    }
                    else
                    {
                        model = new qenrefvalidation();
                        model.qenid = (Int32)qenid;
                       
                    }
                    ViewBag.qenID_ = qenid;
                    return View(model);
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRefrenceInfo(qenrefvalidation refinfo)
        {
           try
            {
                List<qenReference> refrence = db.qenReferences.Where(ex => ex.qenID == refinfo.qenid).ToList();
                qenReference ref1 = null;
                qenReference ref2 = null;
                if (refrence.Count > 0 && refrence != null)
                {
                    ref1 = refrence[0];
                    ref2 = refrence[1];
                    if (ref1 != null)
                    {
                        ref1.qenRefEmail = refinfo.qenRefEmail1;
                        ref1.qenRefCompany = refinfo.qenRefCompany1;
                        ref1.qenRefName = refinfo.qenRefName1;
                        ref1.qenRefPhone = refinfo.qenRefPhone1;
                        ref1.qenRefPosition = refinfo.qenRefPosition1;
                        ref1.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        ref2.qenID = Convert.ToInt32(refinfo.qenid);
                        db.SaveChanges();
                    }
                    if (ref2 != null)
                    {
                        ref2.qenRefEmail = refinfo.qenRefEmail2;
                        ref2.qenRefCompany = refinfo.qenRefCompany2;
                        ref2.qenRefName = refinfo.qenRefName2;
                        ref2.qenRefPhone = refinfo.qenRefPhone2;
                        ref2.qenRefPosition = refinfo.qenRefPosition2;
                        ref2.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                        ref2.qenID = Convert.ToInt32(refinfo.qenid);
                        db.SaveChanges();
                    }

                }
                else
                {
                    ref1 = new qenReference();
                    ref2 = new qenReference();
                    ref1.qenRefEmail = refinfo.qenRefEmail1;
                    ref1.qenRefCompany = refinfo.qenRefCompany1;
                    ref1.qenRefName = refinfo.qenRefName1;
                    ref1.qenRefPhone = refinfo.qenRefPhone1;
                    ref1.qenRefPosition = refinfo.qenRefPosition1;
                    ref1.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    ref1.qenID = Convert.ToInt32(refinfo.qenid);
                    ref2.qenRefEmail = refinfo.qenRefEmail2;
                    ref2.qenRefCompany = refinfo.qenRefCompany2;
                    ref2.qenRefName = refinfo.qenRefName2;
                    ref2.qenRefPhone = refinfo.qenRefPhone2;
                    ref2.qenRefPosition = refinfo.qenRefPosition2;
                    ref2.dataIsCreated = BaseUtil.GetCurrentDateTime();
                    ref2.qenID = Convert.ToInt32(refinfo.qenid);

                    db.qenReferences.Add(ref1);
                    db.qenReferences.Add(ref2);
                    db.SaveChanges();
                }

                return RedirectToAction("CareerObjective",new { qenID = refinfo.qenid});
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(refinfo.qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }
        public ActionResult thankyou()
        {
            return View();
        }
        //public ActionResult ShowAllDetails(int qenid)
        //{
        //    ResumeModel model = new ResumeModel();
        //    qendidateList personal = db.qendidateLists.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    qenSecondary s = db.qenSecondaries.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    qenHigherSecondary hs = db.qenHigherSecondaries.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    qendidateGraduation g = db.qendidateGraduations.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    qendidatePGraduation pg = db.qendidatePGraduations.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    List<qenEmpDetail> emp = db.qenEmpDetails.Where(ex => ex.qenID == qenid).ToList();
        //    qendidatePHD phd = db.qendidatePHDs.Where(ex => ex.qenID == qenid).FirstOrDefault();
        //    List<qenReference> refrences = db.qenReferences.Where(ex => ex.qenID == qenid).ToList();
        //    model.personainfo = personal;
        //    AcademicModel academic = new AcademicModel();
        //    academic.graduation = g != null ? g : new qendidateGraduation();
        //    academic.hsecondary = hs != null ? hs : new qenHigherSecondary();
        //    academic.secondary = s != null ? s : new qenSecondary();
        //    academic.pgraduation = pg != null ? pg : new qendidatePGraduation();
        //    model.educationinfo = academic;
        //    model.employmentinfo = emp != null ? emp : new List<qenEmpDetail>();
        //    model.refrences = refrences != null ? refrences : new List<qenReference>();
        //    model.phdinfo = phd;
        //    return View(model);
        //}
        public PartialViewResult EmploymentAddPop(int? empno, int? qenid)
        {
            qenEmpDetail emp = null;
            if (empno != null && empno > 0)
            {
                emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == empno).FirstOrDefault();
            }
            else
            {
                emp = new qenEmpDetail();
                emp.qenID = Convert.ToInt32(qenid);
            }
            return PartialView("_PartialPopUpEmployment", emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmploymentDetails(qenEmpDetail model)
        {try { 
            qenEmpDetail emp = null;
            if (model.qenEmploymentNum != 0)
            {
                emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == model.qenEmploymentNum).FirstOrDefault();
                if (emp != null)
                {
                    emp.qenPosition = model.qenPosition;
                    emp.qenEmpFrom = model.qenEmpFrom;
                    emp.qenEmpTo = model.qenEmpTo;
                    emp.qenSalary = model.qenSalary;
                    emp.CompanyName = model.CompanyName;
                    emp.jobDescription = model.jobDescription;
                    emp.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                    db.SaveChanges();
                }
            }
            else
            {
                model.dataIsCreated = BaseUtil.GetCurrentDateTime();
                db.qenEmpDetails.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("EmplomentInfo", new { qenid = model.qenID });
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(model.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }



        public ActionResult Deletejob(Int32 empno)
        {
            qenEmpDetail emp = null;
            emp = db.qenEmpDetails.Where(ex => ex.qenEmploymentNum == empno).FirstOrDefault();
            if (emp != null)
            {
                db.qenEmpDetails.Remove(emp);
                db.SaveChanges();
            }
            return RedirectToAction("EmplomentInfo", new { qenid = emp.qenID });
        }


        public ActionResult certifications(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    List<qendidatePHD> qenCertificationsList = null;

                    qenCertificationsList = db.qendidatePHDs.Where(ex => ex.qenID == qenid).ToList();

                    ViewBag.qenID = qenid;
                    return View(qenCertificationsList);
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error : " + ex.Message.ToString();
                return RedirectToAction("Error");
            }
        }

        public ActionResult DeleteCertifications(long phdid)
        {
            qendidatePHD emp = null;
            emp = db.qendidatePHDs.Where(ex => ex.phdid == phdid).FirstOrDefault();
            if (emp != null)
            {
                db.qendidatePHDs.Remove(emp);
                db.SaveChanges();
            }
            return RedirectToAction("EmplomentInfo", new { qenid = emp.qenID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhdInfo(qendidatePHD model)
        {
            try
            {
                if (model.courseField != null && model.courseField.Trim() != "")
                {
                    qendidatePHD phd = null;
                    if (model.phdid != 0)
                    {
                        phd = db.qendidatePHDs.Where(ex => ex.phdid == model.phdid).FirstOrDefault();
                        if (phd != null)
                        {
                            phd.courseField = model.courseField;
                            phd.phdRemarks = model.phdRemarks;
                            phd.phdTitle = model.phdTitle;
                            phd.phdStart = model.phdStart;
                            phd.phdEnd = model.phdEnd;
                            phd.collegeName = model.collegeName;
                            phd.collegeUniversity = model.collegeUniversity;
                            phd.dataIsUpdated = BaseUtil.GetCurrentDateTime();
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        model.dataIsCreated = BaseUtil.GetCurrentDateTime();
                        db.qendidatePHDs.Add(model);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("certifications", new { qenid = model.qenID });
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(model.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }

        }

      
        public PartialViewResult certificationsAddPop(int? phdid, int? qenid)
        {
            qendidatePHD emp = null;
            if (phdid != null && phdid > 0)
            {
                emp = db.qendidatePHDs.Where(ex => ex.phdid == phdid).FirstOrDefault();
            }
            else
            {
                emp = new qendidatePHD();
                emp.qenID = Convert.ToInt32(qenid);
            }
            return PartialView("_PartialAddCertifications", emp);
        }

        public ActionResult skills(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    ViewBag.qenID = qenid;
                    var qenSkills = db.qenSkills.Include(q => q.skill).Where(q => q.qenID == qenid);

                    return View(qenSkills.ToList());
                }
                else
                {
                    TempData["msg"] = "Invalid user to update record";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error : " + ex.Message.ToString();
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkills(qenSkillName model)
        {
            try
            {
                qenSkill oqenSkill = null;
                oqenSkill = db.qenSkills.Where(ex => ex.qenSkillsID == model.qenSkillsID).FirstOrDefault();
                if (oqenSkill != null)
                {
                    int skill_id = checkValuExist(model.skillName.ToString());
                    oqenSkill.skillsID = skill_id;
                    oqenSkill.yearOfExp = model.yearOfExp;
                    oqenSkill.qenID = model.qenID;
                    oqenSkill.qenSkillsID = model.qenSkillsID;
                    db.Entry(oqenSkill).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    oqenSkill = new qenSkill();
                    int skill_id = checkValuExist(model.skillName.ToString());
                    oqenSkill.skillsID = skill_id;
                    oqenSkill.yearOfExp = model.yearOfExp;
                    oqenSkill.qenID = model.qenID;
                    db.qenSkills.Add(oqenSkill);
                    db.SaveChanges();
                }

                return RedirectToAction("skills", new { qenid = model.qenID });
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();

                oapp_error_log.user_id = Convert.ToInt32(model.qenID);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public PartialViewResult skillsAddPop(int? qenSkillsID, int? qenid)
        {

            if (qenSkillsID != null && qenSkillsID > 0)
            {
                var emp = db.qenSkills.Include(e => e.skill).Where(ex => ex.qenSkillsID == qenSkillsID).FirstOrDefault();
                qenSkillName oqenSkillName = new qenSkillName();
                oqenSkillName.skillID = emp.skillsID;
                oqenSkillName.qenID = Convert.ToInt32(emp.qenID);
                oqenSkillName.qenSkillsID = Convert.ToInt32(emp.skillsID);
                oqenSkillName.skillName = emp.skill.skillName;
                oqenSkillName.yearOfExp = emp.yearOfExp;
                return PartialView("_partialAddSkills", oqenSkillName);
            }
            else
            {
                qenSkillName oqenSkillName = new qenSkillName();
                return PartialView("_partialAddSkills", oqenSkillName);
            }

        }


        public JsonResult select_fillSkills(string Prefix)
        {
            //Note : you can bind same list from database  

            var skillName = db.skills.Where(m => m.skillName.StartsWith(Prefix)).Select(x => new { x.skillName, x.skillsID });
            return Json(skillName, JsonRequestBehavior.AllowGet);
        }
        public int checkValuExist(string skill_)
        {

            var result = db.skills.Where(e => e.skillName == skill_).Select(x => new { x.skillsID }).SingleOrDefault();
            if (result == null)
            {
                skill oskill = new skill();
                oskill.skillName = skill_;
                db.skills.Add(oskill);
                db.SaveChanges();
                return oskill.skillsID;
            }
            else
            {
                return result.skillsID;
            }
        }

        public ActionResult DeleteqenSkills(long qenSkillsID)
        {
            qenSkill oqenSkill = null;
            oqenSkill = db.qenSkills.Where(ex => ex.qenSkillsID == qenSkillsID).FirstOrDefault();
            if (oqenSkill != null)
            {
                db.qenSkills.Remove(oqenSkill);
                db.SaveChanges();

            }
            //return RedirectToAction("skills", new { qenid = oqenSkill.qenSkillsID });
            return RedirectToAction("skills", new { qenid = qenSkillsID });
        }



        public ActionResult Error()
        {
            ViewBag.error = TempData["msg"].ToString();
            return View();
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        [HttpGet]
        public ActionResult CareerObjective(string qenID)
        {
            long qenid = 0;
            try
            {
                qenid = (long)Convert.ToInt64(qenID);
                if (qenid == (long)Session["LoginID"])
                {
                    var careerObjective = db.qendidateLists.Where(e => e.qenID == qenid).Select(e=>new { e.CareerObjective}).FirstOrDefault();

                    if (careerObjective.CareerObjective != null)
                    {
                        ViewBag.careerObjective = careerObjective.CareerObjective.ToString();
                    }
                    else {
                        ViewBag.careerObjective = "";
                    }
                    ViewBag.qenID = qenid;
                    return View();
                }
              else
                {
                TempData["msg"] = "Invalid user to update record";
                return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();
                oapp_error_log.user_id = Convert.ToInt32(qenid);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public ActionResult CareerObjective(FormCollection frm)
        {
            try
            {

                long qenid = 0;
                string txt = string.Empty;
                qenid = Convert.ToInt64(frm["hdn_qenID"]);
                txt = frm["CareerObjective"].ToString();

                qendidateList oqendidateList = new qendidateList();
                oqendidateList = db.qendidateLists.Find(qenid);
                oqendidateList.CareerObjective = txt;
                db.SaveChanges();
                return RedirectToAction("thankyou");
                }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message.ToString();
                app_error_log oapp_error_log = new app_error_log();
                 oapp_error_log.user_id = Convert.ToInt32(frm["hdn_qenID"]);
                oapp_error_log.error_message = ex.Message.ToString();
                oapp_error_log.ApplicationName = "NewsLetter";
                oapp_error_log.created_date = DateTime.Now;
                db.app_error_log.Add(oapp_error_log);
                db.SaveChanges();
                return RedirectToAction("Error");
    }
}
    }
}


























