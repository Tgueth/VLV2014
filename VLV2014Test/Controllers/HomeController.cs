using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VLV2014Test.Models;

using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

namespace VLV2014Test.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager dataMgr = null;
        private IEvent eventID;

        public HomeController(IDataManager dataMgr, IEvent eventID)
        {
            this.dataMgr = dataMgr;
            this.eventID = eventID;
        }

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
            WineList red = dataMgr.GetWinesByClass(eventID, "Red Wine");
            WineList white = dataMgr.GetWinesByClass(eventID, "White Wine");
            WineList dessert = dataMgr.GetWinesByClass(eventID, "Dessert Wine");
            
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

        public ActionResult PurchaseTicketNS()
        {
            return View();
        }

        public ActionResult Sponsors()
        {
            Sponsors AllSponsors = dataMgr.GetSponsors(eventID);
            return View(AllSponsors);
        }

        public ActionResult AuctionItems()
        {
            AuctionGroup auctions = new AuctionGroup();

            RevenueItems liveAuction = dataMgr.GetRevenueItemsByType(eventID, "Live Auction");
            auctions.Add(liveAuction);

            RevenueItems silentAuction = dataMgr.GetRevenueItemsByType(eventID, "Silent Auction");
            auctions.Add(silentAuction);

            return View(auctions);
        }

        public ActionResult EventSchedule()
        {
            EventActivities acts = dataMgr.GetEventActivities(eventID);

            return View(acts);
        }

        public ActionResult SponsorForm()
        {
            return File("../Downloads/Viva La Vino 2014 Sponsorship and Donation form.pdf", "application/pdf","VLV2014SponsorForm.pdf");
        }

        public ActionResult Foods()
        {
            RevenueItems foods = dataMgr.GetRevenueItemsByType(eventID, "Foods");
            Tables foodTables = dataMgr.GetTablesForAuctionSaleType(eventID, "Foods");

            return View(foodTables);
        }

        public ActionResult BidderSignup()
        {
            return View();
        }
    }
}