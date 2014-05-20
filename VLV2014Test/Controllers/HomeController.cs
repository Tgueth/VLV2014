using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

namespace VLV2014Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Wines()
        {
            WineList red = MvcApplication.SiteDataMgr.GetWinesByClass(CommonSiteInfo.EventIdent, "Red Wine");
            WineList white = MvcApplication.SiteDataMgr.GetWinesByClass(CommonSiteInfo.EventIdent, "White Wine");
            WineList dessert = MvcApplication.SiteDataMgr.GetWinesByClass(CommonSiteInfo.EventIdent, "Dessert Wine");
            
            WineGroups wineGroups = new WineGroups();
            wineGroups.Add(red);
            wineGroups.Add(white);
            wineGroups.Add(dessert);

            return View(wineGroups);
        }

        public ActionResult PurchaseTicket()
        {
            return View();
        }

        public ActionResult Sponsors()
        {
            return View();
        }

        public ActionResult AuctionItems()
        {
            CommonSiteInfo siteInfo = CommonSiteInfo.GetSiteData(this);

            return View(siteInfo.Auctions);
        }

        public ActionResult EventSchedule()
        {
            return View();
        }

        public ActionResult SponsorForm()
        {
            return File("../Downloads/Viva La Vino 2014 Sponsorship and Donation form.pdf", "application/pdf","VLV2014SponsorForm.pdf");
        }
    }
}