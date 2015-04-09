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

        /// <summary>
        /// HomeController
        /// </summary>
        /// <param name="dataMgr"></param>
        /// <param name="eventID"></param>
        /// <param name="items"></param>
        public HomeController(IDataManager dataMgr, IEvent eventID, MenuItems items)
        {
            this.dataMgr = dataMgr;
            this.eventID = eventID;
            menuItems = items;
            sponsorGroups = dataMgr.GetAllSponsorsByLevel(eventID);
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
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

        
        /// <summary>
        /// Wines
        /// </summary>
        /// <returns></returns>
        public ActionResult Wines()
        {
            ImageLink redWineBackground = dataMgr.GetImageLink(153);
            ImageLink whiteWineBackground = dataMgr.GetImageLink(155);
            ImageLink dessertWineBackground = dataMgr.GetImageLink(156);
            ImageLink noImage = dataMgr.GetImageLink(154);

            WineList red = dataMgr.GetWinesByClass(eventID, "Red Wine");
            WineList white = dataMgr.GetWinesByClass(eventID, "White Wine");
            WineList dessert = dataMgr.GetWinesByClass(eventID, "Dessert Wine");
            
            WebPageItemsGroup wineWebGroups = new WebPageItemsGroup();

            wineWebGroups.Add(LoadWines(dessert, "Red Wines", redWineBackground, dataMgr.GetImageLink(152), noImage));

            wineWebGroups.Add(LoadWines(white, "White Wines", whiteWineBackground, dataMgr.GetImageLink(152), noImage));

            wineWebGroups.Add(LoadWines(dessert,"Dessert Wines",dessertWineBackground, dataMgr.GetImageLink(152), noImage));


            return View(wineWebGroups);
        }

        /// <summary>
        /// LoadWines
        /// </summary>
        /// <param name="wines"></param>
        /// <param name="wineType"></param>
        /// <param name="background"></param>
        /// <param name="image"></param>
        /// <param name="noImage"></param>
        /// <returns></returns>
        private WebPageItems LoadWines(WineList wines, string wineType, ImageLink background, ImageLink image, ImageLink noImage)
        {
            WebPageItems webWines = new WebPageItems();

            if (wines == null || wines.Count <= 0)
            {
                WebPageItem wineItem = new WebPageItem(dataMgr, eventID, wineType, background, image, "List of Dessert wines is not yet available", "", "Please check back later.", "", "", "", "", "", "", "");
                webWines.Add(wineItem);
            }
            else
            {
                foreach (Wine wine in wines)
                {
                    WebPageItem wineItem = new WebPageItem(dataMgr, eventID, wineType, background, (wine.PictureLink != null ? wine.PictureLink : noImage), wine.ItemName, "", wine.Description, "", "", "", "", "", "", "");
                    webWines.Add(wineItem);
                }
            }

            return webWines;
        }

        /// <summary>
        /// PurchaseTicket
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseTicket()
        {
            return View(eventID);
        }

        /// <summary>
        /// PurchaseTicketNS
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseTicketNS()
        {
            return View();
        }

        /// <summary>
        /// Sponsors
        /// </summary>
        /// <returns></returns>
        public ActionResult Sponsors()
        {
            WebPageItemsGroup allSponsors = new WebPageItemsGroup();
            WebPageItems sponsors = new WebPageItems();
            WebPageItem sponsor = null;
            ImageLink img1 = dataMgr.GetImageLink(153);
            ImageLink img2 = dataMgr.GetImageLink(152);
            string link = null;
            string aLink = null;

            RevenueItems sponsorships = dataMgr.GetRevenueItemsByType(eventID, "Sponsorship");
            int length = 0;
            // get length of longest sponsor level so we can try to make web page tabs even
            foreach(RevenueItem item in sponsorships)
            {
                length = System.Math.Max(item.ItemSubType.Length, length);
            }

            foreach(RevenueItem sponsorshipLevel in sponsorships)
            {
                Sponsors webSponsors = dataMgr.GetSponsorsByLevel(eventID, sponsorshipLevel.Item);
                if ( webSponsors != null &&  webSponsors.Count > 0)
                {
                    sponsors = new WebPageItems();
                    foreach(Sponsor webSponsor in webSponsors)
                    {
                        if (webSponsor.SponsorImage != null && webSponsor.SponsorImage.Image != null)
                        {
                            link = "<a href=\"" + webSponsor.SponsorImage.Link + "\" target=\"_blank\"><img src=\"" + webSponsor.SponsorImage.Image + "\" alt=\"\"></a>";

                        }
                        else
                        {
                            link = "<img src=\"../images/Image_Not_Available.jpg\" alt=\"\" height=\"50\" width=\"50\" >";
                        }
                        aLink = "<p style=\"font-size:12pt\"><a href=\"" + webSponsor.SponsorImage.Link + "\" target=\"_blank\" >Website</a></p>";
                        sponsor = new WebPageItem(dataMgr, eventID, sponsorshipLevel.ItemName, img1, img2, webSponsor.SponsorLevel + " Level Sponsor", webSponsor.SponsorName, link, "", webSponsor.SponsorSecondLine, aLink, "", "", "", "");
                        sponsors.Add(sponsor);
                    }

                    if ( sponsors.Count > 0)
                    {
                        allSponsors.Add(sponsors);
                    }
                }
            }

            SponsorsView view = new SponsorsView();
            view.EventID = eventID;
            view.SponsorsGrouping = allSponsors;

            return View(view);
        }

        /// <summary>
        /// AuctionItems
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// EventSchedule
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BidderSignup()
        {
            return View();
        }
    }
}