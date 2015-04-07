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
        public static MenuItems menuItems = null;
        public static SponsorGroups sponsorGroups = null;

        public HomeController(IDataManager dataMgr, IEvent eventID, MenuItems items)
        {
            this.dataMgr = dataMgr;
            this.eventID = eventID;
            menuItems = items;
            sponsorGroups = dataMgr.GetAllSponsorsByLevel(eventID);
        }

        public ActionResult Index()
        {
            WebPageItemsGroup groups = dataMgr.GetWebPageGroup(eventID, "Index");
            IndexView indexView = new IndexView();
            indexView.EventID = eventID;
            indexView.AllSponsors = sponsorGroups;
            indexView.WebItems = groups;
            return View(indexView);
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
            return View(eventID);
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
            if (liveAuction != null)
            { auctions.Add(liveAuction); }

            RevenueItems silentAuction = dataMgr.GetRevenueItemsByType(eventID, "Silent Auction");
            if (silentAuction != null)
            { auctions.Add(silentAuction); }

            if (auctions.EventID == null) auctions.EventID = this.eventID;

            return View(auctions);
        }

        public ActionResult EventSchedule()
        {
            EventActivities acts = dataMgr.GetEventActivities(eventID);
            if (acts == null) acts = new EventActivities();
            if (acts.EventID == null) acts.EventID = eventID;

            return View(acts);
        }

        public ActionResult SponsorForm()
        {
            return File("../Downloads/Viva La Vino 2014 Sponsorship and Donation form.pdf", "application/pdf","VLV2014SponsorForm.pdf");
        }

        public ActionResult Foods()
        {
            Tables foodTables = dataMgr.GetTablesForAuctionSaleType(eventID, "Foods");

            if ( foodTables == null )
            { 
                foodTables = new Tables();
            }

            if ( foodTables.EventID == null)
            {
                foodTables.EventID = this.eventID;
            }

            FoodsView view = new FoodsView();
            view.EventID = eventID;
            view.FoodsToShow = foodTables;
            view.AllSponsors = sponsorGroups;

            return View(view);
        }

        public ActionResult BidderSignup()
        {
            return View();
        }
    }
}