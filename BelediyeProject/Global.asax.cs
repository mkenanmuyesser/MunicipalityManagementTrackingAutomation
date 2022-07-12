using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BelediyeProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            using (var entity = new DBEntities())
            {
                Exception ex = Server.GetLastError();

                DateTime Tarih = DateTime.Now;
                string Message = ex.Message;
                string Source = ex.Source;
                string StackTrace = ex.StackTrace;
                string ExceptionType = ex.GetType().FullName;

                Guid? kullaniciKey = null;
                var kullaniciData = GirisIslemBS.KullaniciDataGetir(); 
                if (kullaniciData == null)
                {
                    Response.Redirect("/GirisIslem/Index");
                }
                else
                {
                    kullaniciKey = kullaniciData.KullaniciKey;
                }

                string Url = "";
                if (HttpContext.Current != null)
                {
                    Url = HttpContext.Current.Request.Url.AbsoluteUri;
                }

                var programLog = new ProgramLog
                {
                    Tarih = Tarih,
                    Message = Message,
                    Source = Source,
                    StackTrace = StackTrace,
                    ExceptionType = ExceptionType,
                    KullaniciKey = kullaniciKey,
                    Url = Url,
                };
                entity.ProgramLogs.Add(programLog);
                entity.SaveChanges();

                //Server.ClearError();
            }
        }
    }
}
