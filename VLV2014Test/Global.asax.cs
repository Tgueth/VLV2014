using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

namespace VLV2014Test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static DataManager SiteDataMgr = null;
        string connString = null;
        DbProviderFactory factory = null;
        public static CommonSiteInfo SiteInfo = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            connString = System.Configuration.ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ConnectionString;
            factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ProviderName);
            SiteDataMgr = new DataManager(factory, connString);
            string strEvent = System.Configuration.ConfigurationManager.AppSettings["EventID"].ToString();
            Event eventID = SiteDataMgr.GetEvent(Convert.ToInt32(strEvent));
            SiteInfo = new CommonSiteInfo(SiteDataMgr, eventID);
        }
    }
}
