﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.Reflection;
using System.Web.Routing;

namespace NewLetter.Models
{
    public class baseClass :oriondbEntities1
    {
        #region AutoGenerated Password
        public static string GetRandomPasswordString(int pwdLength)
        {
            int asciiZero;
            int asciiNine;
            int asciiA;
            int asciiZ;
            int count = 0;
            int randNum;
            string randomString;

            System.Random rRandom = new System.Random(System.DateTime.Now.AddMinutes(0).Millisecond);

            asciiZero = 48;
            asciiNine = 57;
            asciiA = 64;
            asciiZ = 90;

            randomString = "";
            while ((count < pwdLength))
            {
                if (count % 2 == 0)
                {
                    randNum = rRandom.Next(asciiZero, asciiNine);
                }
                else
                {
                    randNum = rRandom.Next(asciiA, asciiZ);
                }
                if (((randNum >= asciiZero) && (randNum <= asciiNine)) || ((randNum >= asciiA) && (randNum <= asciiZ)))
                {
                    randomString = (randomString + ((char)(randNum)));
                    count = (count + 1);
                }
            }
            return randomString;
        }
        #endregion
        #region Configuration
        public static string GetWebConfigValue(string Name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Name].ToString(); ;
        }

        #endregion
        #region Send Mail
        public static void SendMail(String EmailIDTo, String SubjectText, String Body, String[] attachments = null)
        {
            try
            {

                string EMAIL_SENT = GetWebConfigValue("EMAIL_SENT");
                string COMPANY_EMAIL = GetWebConfigValue("COMPANY_EMAIL");
                string COMPANY_EMAIL_PWD = GetWebConfigValue("COMPANY_EMAIL_PWD");
                string Host = GetWebConfigValue("Host");

                string CC = GetWebConfigValue("CC");
                //string BCC = GetWebConfigValue("BCC");

                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(COMPANY_EMAIL, SubjectText);
                smtpClient.Host = Host;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(COMPANY_EMAIL, COMPANY_EMAIL_PWD);

                message.From = fromAddress;
                message.To.Add(Convert.ToString(EmailIDTo.Trim()));
                //message.CC.Add(Convert.ToString(CC));
                // message.Bcc.Add(Convert.ToString(BCC));

                StringBuilder sb = new StringBuilder();
                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        if (item != null)
                            message.Attachments.Add(new Attachment(item));
                    }
                }
                message.Subject = SubjectText;
                message.IsBodyHtml = true;
                message.Body = Body;// +sb.ToString();
                if (EMAIL_SENT == "Y")
                {
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion



    }
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
    }

    public enum AdminInfo
    {
        UserID, FullName, UserPhoto, EmailID, Mobile, role_id, LoginID, IP, Last_Login_Date, latitude, longitude, IPTrackerID,

        SuperAdmin, DataEntryoperator, tktcategory, RoleBit,companyID,employerID,
        Session_Person
    }


   
 



    public static class BaseUtil
    {
        private static baseClass db = new baseClass();
        // private static CommonUtil commonUtil = new CommonUtil();
        public static string JobpostOption { get; set; }
        #region Date & Time
        public static String WeekNumber(DateTime dt)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);

            return "Week-" + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        }

        public static String WeekNumberStartDate(DateTime dt)
        {

            DayOfWeek day = DayOfWeek.Sunday;
            int diff = dt.DayOfWeek - day;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date.ToString("dd-MMM-yyyy");

        }

        public static DateTime GetCurrentDateTime()
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Now.AddMinutes(diffMinutes);
        }
        public static DateTime GetCalculatedDateTime(int day)
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Now.AddMinutes(diffMinutes).AddDays(day);
        }
        public static DateTime GetTodayDate()
        {
            Int32 diffMinutes = Convert.ToInt32(GetWebConfigValue("MIN_DIFF"));
            return System.DateTime.Today.AddMinutes(diffMinutes).Date;
        }
        #endregion

        #region Configuration
        public static string GetWebConfigValue(string Name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[Name].ToString(); ;
        }

        #endregion

        #region Set Property
        public static void SetProperty(object p, string propName, object value)
        {
            Type t = p.GetType();
            PropertyInfo info = t.GetProperty(propName);
            if (info == null)
                return;
            if (!info.CanWrite)
                return;
            info.SetValue(p, value, null);
        }
        #endregion

        #region Capture Error
        public static int CaptureErrorValues(Exception exception)
        {
            int errorLogID = 0;
            #region Capture the Values
            StringBuilder errrorMessage = new StringBuilder();

            string FormValues = String.Empty;

            string SessionValues = String.Empty;
            if (HttpContext.Current.Session != null)
            {
                foreach (var item in HttpContext.Current.Session.Keys)
                {
                    SessionValues = SessionValues + item.ToString() + " : " + HttpContext.Current.Session[item.ToString()] + "<br>";
                }
            }

            string RawUrl = HttpContext.Current.Request.RawUrl;
            string RequestType = HttpContext.Current.Request.RequestType;
            string UserAgent = HttpContext.Current.Request.UserAgent;
            string BrowserName = ((System.Web.Configuration.HttpCapabilitiesBase)(HttpContext.Current.Request.Browser)).Browser;


            errrorMessage.Append("<b>Simplysafe Tech Support Error</b><br><br>");

            errrorMessage.Append("<b>Error Date  :</b>" + System.DateTime.Now + "<br>");
            errrorMessage.Append("<b>Server Name  :</b>" + System.Environment.MachineName + "<br>");
            errrorMessage.Append("<b>Application Url :</b>" + RawUrl + "<br>");
            errrorMessage.Append("<b>Request Type :</b>" + RequestType + "<br>");
            errrorMessage.Append("<b>User Agent :</b>" + UserAgent + "<br><br>");
            errrorMessage.Append("<b>BrowserName :</b>" + BrowserName + "<br><br>");

            errrorMessage.Append("<b>Exception InnerException :</b>" + exception.InnerException + "<br><br>");
            errrorMessage.Append("<b>Exception Message :</b>" + exception.Message + "<br><br>");
            errrorMessage.Append("<b>Exception Source :</b>" + exception.Source + "<br><br>");

            errrorMessage.Append("<u><b>Posted Form Values</b></u><br>");
            errrorMessage.Append(FormValues);

            errrorMessage.Append("<br><br><u><b>Session Values Values</b></u><br>");
            errrorMessage.Append(SessionValues);

            errrorMessage.Append("<br><br><u><b>Exception Stack Trace:</b></u><br>" + exception.StackTrace);

            #endregion

            try
            {
                Int32 userID = 0;
                if (HttpContext.Current.Session != null)
                {
                    userID = HttpContext.Current.Session[AdminInfo.UserID.ToString()] != null ? Convert.ToInt32(HttpContext.Current.Session[AdminInfo.UserID.ToString()].ToString()) : 0;
                }
                DateTime currDateTime = GetCurrentDateTime();

                #region Insert Into APP_ERROR_LOG

                app_error_log errorlog = new app_error_log();
                errorlog.error_message = errrorMessage.ToString();
                if (userID > 0)
                {
                    errorlog.user_id = userID;
                }
                else
                {
                    errorlog.user_id = null;

                }
                errorlog.created_date = currDateTime;

                db.app_error_log.Add(errorlog);
                db.SaveChanges();

                errorLogID = errorlog.error_log_id;
                #endregion

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {

            }
            return errorLogID;

        }
        public static string GetValidationMessage(ModelStateDictionary modelState)
        {
            string msgResult = "";
            for (int i = 0; i < modelState.ToList().Count; i++)
            {
                if (modelState.ToList()[i].Value.Errors.Count > 0)
                {
                    msgResult += string.Format("{0}~{1};", modelState.ToList()[i].Key.ToString(), modelState.ToList()[i].Value.Errors[0].ErrorMessage);
                }
            }
            return msgResult;
        }
        #endregion

        #region Session
        public static void SetSessionValue(String Key, String Value)
        {
            HttpContext.Current.Session[Key] = Value;
        }
        public static String GetSessionValue(String Key)
        {
            return HttpContext.Current.Session[Key] != null ? HttpContext.Current.Session[Key].ToString() : string.Empty;
        }

        #endregion

        #region Accesible Pages

     
        public static List<string> ListControllerExcluded()
        {
            List<string> list = new List<string>() { "BASE", "JSON", "HOME", "USERREGISTRATION" };
            return list;
        }
       

        #endregion


       


        public static bool IsAuthenticated()
        {
            return string.IsNullOrEmpty(GetSessionValue(AdminInfo.UserID.ToString())) ? false : true;
        }


        public static string RoleID()
        {
            return Convert.ToString(GetSessionValue(AdminInfo.role_id.ToString()));
        }
        #region Application Path

        static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        static HttpRequest Request
        {
            get { return Context.Request; }
        }


        public static UrlHelper GetUrlHelper()
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public static string GetActionPath(string controllerActionName) //Ex: "Category/Edit"
        {
            return GetApplicationPath(Request.RequestContext) + string.Format("/{0}", controllerActionName);
        }

        public static string GetActionPath(string controllerName, string actionName)
        {
            return GetApplicationPath(Request.RequestContext) + string.Format("/{0}/{1}", controllerName, actionName);
        }

        public static string GetApplicationPath(RequestContext requestContext)
        {
            string retPath;
            string httpOrigin = Request.ServerVariables["HTTP_ORIGIN"];
            string applicationPath = Request.ApplicationPath;
            //Approach #1: OK:Post
            //retPath = (applicationPath == "/" ? httpOrigin : httpOrigin + applicationPath);
            //Approach #2 OK:All
            retPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority) + (applicationPath == "/" ? "" : applicationPath);
            return retPath;
        }

        public static string GetApplicationPath()
        {
            string retPath;
            string applicationPath = Request.ApplicationPath;
            retPath = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority) + (applicationPath == "/" ? "" : applicationPath);
            return retPath;
        }
        public static string GetLoginID()
        {
            return SessionUtil.GetLoginID();
        }

        public static string GetCurrentController()
        {
            return Convert.ToString(Request.RequestContext.RouteData.Values["controller"]);
        }
        public static string GetCurrentAction()
        {
            return Convert.ToString(Request.RequestContext.RouteData.Values["action"]);
        }

        public static List<string> GetControllerNames()
        {
            List<string> controllerNames = new List<string>();
            GetSubClasses<Controller>().ForEach(
                type => controllerNames.Add(type.Name));
            return controllerNames;
        }

        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public static List<string> GetControllerActionNames(Type t)
        {
            return t.GetMethods().Where(m => m.ReturnType == typeof(ActionResult))
                .Select(m => m.Name).Distinct().ToList();
        }
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        #endregion
      

    }


    public class HtmlHelperMultiSelect
    {
        public HtmlHelperMultiSelect()
        {
        }
        public HtmlHelperMultiSelect(string Id)
        {
            this.Id = Id;
        }
        public HtmlHelperMultiSelect(string Id, IList<SelectListItem> list, BaseValidation baseValidation)
        {
            this.Id = Id;
            this.List = list;
            this.BaseValidation = baseValidation;
        }
        public string Id { get; set; }
        public IList<SelectListItem> List { get; set; }
        public BaseValidation BaseValidation { get; set; }
        public string SelectedValues { get; set; }
        public string Caption { get; set; }
    }
    public class BaseValidation
    {
        public BaseValidation()
        {

        }
        public BaseValidation(bool isRequired, string requiredText, string validationId = null)
        {
            this.IsRequired = isRequired;
            this.RequiredText = requiredText;
            this.ValidationId = validationId;
        }
        public bool IsRequired { get; set; }
        public string ValidationId { get; set; }
        public string RequiredText { get; set; }
        public HtmlString GetValidationString()
        {
            var vs = "";
            if (this.ValidationId != null)
            {
                string ret = " " + BaseConst.VALIDATION_ISREQUIRED + "='{0}' " + BaseConst.VALIDATION_REQ_MSG + "='{1}' " + BaseConst.VALIDATION_ID + "='{2}' ";
                vs = string.Format(ret, Convert.ToString(this.IsRequired).ToLower(), this.RequiredText, this.ValidationId);
            }
            else
            {
                string ret = " " + BaseConst.VALIDATION_ISREQUIRED + "='{0}' " + BaseConst.VALIDATION_REQ_MSG + "='{1}' ";
                vs = string.Format(ret, Convert.ToString(this.IsRequired).ToLower(), this.RequiredText);
            }
            return new HtmlString(vs);
        }
    }
    public static class BaseConst
    {
        public const string VALIDATION_ISREQUIRED = "data-valc-isrequired";
        public const string VALIDATION_REQ_MSG = "data-valc-required-msg";
        public const string VALIDATION_ID = "data-valc-validation-id";

    }

    public static class SessionUtil
    {
        public static int GetUserID()
        {
            return Convert.ToInt32(BaseUtil.GetSessionValue(AdminInfo.UserID.ToString()));
        }
        public static string GetLoginID()
        {
            return BaseUtil.GetSessionValue(AdminInfo.LoginID.ToString());
        }

    }

    #region WebGrid
    //public static class WebGridBase
    //{
    //    public static WebGrid Init(ParmInGridInit parmIn)
    //    {
    //        WebGrid grid = new WebGrid(null, canPage: parmIn.CanPage, rowsPerPage: parmIn.RowPerPage,
    //        selectionFieldName: "selectedRow", ajaxUpdateContainerId: parmIn.AjaxContainerID);
    //        grid.Pager(WebGridPagerModes.FirstLast);
    //        grid.Bind(parmIn.Source);
    //        return grid;
    //    }
    //    public static IHtmlString GetWebGridHtml(WebGrid grid, WebGridColumn[] columns = null)
    //    {
    //        return grid.GetHtml(tableStyle: "table table-bordered",
    //        headerStyle: "DataGridHeader",
    //        alternatingRowStyle: "DataGridrowb",
    //        rowStyle: "DataGridrowa",
    //        selectedRowStyle: "DataGridHeader",
    //        mode: WebGridPagerModes.FirstLast | WebGridPagerModes.NextPrevious | WebGridPagerModes.Numeric,
    //        firstText: "First", lastText: "Last",
    //        previousText: "Prev", nextText: "Next",
    //        numericLinksCount: 10,
    //        columns: columns);
    //    }
    //}

    //public class ParmInGridInit
    //{
    //    public string AjaxContainerID { get; set; }
    //    public IEnumerable<dynamic> Source { get; set; }
    //    public int RowPerPage { get; set; }
    //    public bool CanPage { get; set; }
    //    public ParmInGridInit()
    //    {
    //        RowPerPage = 50;
    //        CanPage = true;
    //    }
    //}
    #endregion
}
