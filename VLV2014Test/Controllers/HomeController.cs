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
            ImageLink redWineBackground = dataMgr.GetImageLink(153);
            ImageLink whiteWineBackground = dataMgr.GetImageLink(155);
            ImageLink dessertWineBackground = dataMgr.GetImageLink(156);
            ImageLink noImage = dataMgr.GetImageLink(154);

            WineList red = dataMgr.GetWinesByClass(eventID, "Red Wine");
            WineList white = dataMgr.GetWinesByClass(eventID, "White Wine");
            WineList dessert = dataMgr.GetWinesByClass(eventID, "Dessert Wine");
            
            WineGroups wineGroups = new WineGroups();
            wineGroups.Add(red);
            wineGroups.Add(white);
            wineGroups.Add(dessert);

            WebPageItemsGroup wineWebGroups = new WebPageItemsGroup();
            WebPageItems redWines = new WebPageItems();
            if (red == null || red.Count <= 0)
            {
                WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "Red Wine", redWineBackground, dataMgr.GetImageLink(152), "List of red wines is not yet available", "", "Please check back later.", "", "", "", "", "", "", "");
                redWines.Add(wineItem);
            }
            else
            {
                foreach (Wine wine in red)
                {
                    WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "Red Wine", redWineBackground, (wine.PictureLink != null ? wine.PictureLink : noImage), wine.ItemName, "", wine.Description, "", "", "", "", "", "", "");
                    redWines.Add(wineItem);
                }
            }
            wineWebGroups.Add(redWines);

            WebPageItems whiteWines = new WebPageItems();
            if (white == null || white.Count <= 0)
            {
                WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "White Wine", whiteWineBackground, dataMgr.GetImageLink(152), "List of white wines is not yet available", "", "Please check back later.", "", "", "", "", "", "", "");
                whiteWines.Add(wineItem);
            }
            else
            {
                foreach (Wine wine in white)
                {
                    WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "White Wine", whiteWineBackground, (wine.PictureLink != null ? wine.PictureLink : noImage), wine.ItemName, "", wine.Description, "", "", "", "", "", "", "");
                    whiteWines.Add(wineItem);
                }
            }
            wineWebGroups.Add(whiteWines);

            WebPageItems dessertWines = new WebPageItems();
            if (dessert == null || dessert.Count <= 0)
            {
                WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "Dessert Wine", whiteWineBackground, dataMgr.GetImageLink(152), "List of Dessert wines is not yet available", "", "Please check back later.", "", "", "", "", "", "", "");
                dessertWines.Add(wineItem);
            }
            else
            {
                foreach (Wine wine in dessert)
                {
                    WebPageItem wineItem = new WebPageItem(dataMgr, eventID, "Dessert Wine", whiteWineBackground, (wine.PictureLink != null ? wine.PictureLink : noImage), wine.ItemName, "", wine.Description, "", "", "", "", "", "", "");
                    dessertWines.Add(wineItem);
                }
            }
            wineWebGroups.Add(dessertWines);


            return View(wineWebGroups);
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

        /// <summary>
        /// Foods
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Foods()
        {
            WebPageItemsGroup foodGroups = new WebPageItemsGroup();
            WebPageItems foodsOnTable = null;
            WebPageItem foodToShow = null;
            ImageLink background = dataMgr.GetImageLink(151);
            Tables foodTables = dataMgr.GetTablesForAuctionSaleType(eventID, "Foods");

            if ( foodTables == null )
            { 
                foodTables = new Tables();
            }

            if ( foodTables.EventID == null)
            {
                foodTables.EventID = this.eventID;
            }

            if ( foodTables != null && foodTables.Count > 0)
            {
                foreach (Table table in foodTables)
                {
                    foodsOnTable = new WebPageItems();
                    foreach (RevenueItem food in table)
                    {
                        string desc = "Description: " + food.Description;
                        foodToShow = new WebPageItem();
                        foodToShow = new WebPageItem(dataMgr, eventID, food.AssignedTable, background, food.PictureLink, "", food.AssignedTable, "", food.Name, desc, "", "", "", "", "");
                        foodsOnTable.Add(foodToShow);
                    }

                    if (foodsOnTable.Count > 0) foodGroups.Add(foodsOnTable);
                }
            }
            else
            {
                foodsOnTable = new WebPageItems();
                foodToShow = new WebPageItem(dataMgr, eventID, "Food", 150, 152, "List of foods is not yet available", "", "Please check back later.", "", "", "", "", "", "", "");
                foodsOnTable.Add(foodToShow);
                foodGroups.Add(foodsOnTable);
            }

            FoodsView view = new FoodsView();
            view.EventID = eventID;
            view.FoodsToShow = foodGroups;
            view.AllSponsors = sponsorGroups;

            return View(view);
        }

        public ActionResult BidderSignup()
        {
            return View();
        }
    }
}