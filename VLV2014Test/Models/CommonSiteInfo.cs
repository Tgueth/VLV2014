using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

namespace VLV2014Test
{
    public class CommonSiteInfo
    {
        public static Event EventIdent = new Event() { EventID = 1, Description = "Viva La Vino 2012", EventDate = new DateTime(2012, 9, 21), EventLocation = "San Marino Club", EventName = "Viva La Vino 2012", MapLink = null };

        public MenuItems Menu = new MenuItems()
        {
            { new MenuItem("Home Page", "Index", "Home", null, null, true, false,false) },
            { new MenuItem("Event Schedule", "EventSchedule", "Home", null, null, true, false,false) },
            { new MenuItem("Auction Items", "AuctionItems", "Home", null, null, true, false,false) },
            { new MenuItem("Wine List", "WineList", "Home", null, null, true, false,false) },
            { new MenuItem("Purchase Ticket", "PurchaseTicket", "Home", null, null, true, false,false) },
            { new MenuItem("Event Photos 2012", "EventPhotos", "Home", null, null, true, false,false) },
            { new MenuItem("Sponsors List", "SponsorsList", "Home", null, null, true, false,false) },
            { new MenuItem("Donations","Donations","Home",null,null, true, false,false) },
            { new MenuItem("Music", "Music", "Home", null, null, true, false,false) },
            { new MenuItem("Charitable Work", "ShowCharity", "Home", null, null, true, false,false) }
        };

        public Sponsor PresentingSponsor = new Sponsor();
        public Sponsors TitleSponsors = new Sponsors();
        public Sponsors EliteSponsors = new Sponsors();
        public Sponsors ValetSponsor = new Sponsors();
        public Sponsors GoldSponsors = new Sponsors();
        public Sponsors SilverSponsors = new Sponsors();
        public Sponsors RotarySponsors = new Sponsors();

        public SponsorGroups SponsorLevels = new SponsorGroups();

        public AuctionItems SilentAuction = new AuctionItems();
        public AuctionItems LiveAuction = null;

        public AuctionGroup Auctions = new AuctionGroup();

        public CommonSiteInfo()
        {
        }

        public static CommonSiteInfo GetSiteData(Controller ctlr)
        {
            if (MvcApplication.SiteDataMgr == null)
            {
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ConnectionString;
                DbProviderFactory factory = DbProviderFactories.GetFactory(System.Configuration.ConfigurationManager.ConnectionStrings["VivaLaVinoConnectionString"].ProviderName);
                MvcApplication.SiteDataMgr = new DataManager(factory, connString);
            }
            CommonSiteInfo siteInfo = (CommonSiteInfo)ctlr.Session["SiteInfo"];
            string eventID = System.Configuration.ConfigurationManager.AppSettings["EventID"].ToString();
            

            if (siteInfo == null || EventIdent == null)
            {
                siteInfo = new CommonSiteInfo();

                EventIdent = MvcApplication.SiteDataMgr.GetEvent(eventID);
                siteInfo.LiveAuction = MvcApplication.SiteDataMgr.GetAuctionItemsByType(EventIdent, "Live Auction");
                siteInfo.Auctions.Add(siteInfo.LiveAuction);
                siteInfo.SilentAuction = MvcApplication.SiteDataMgr.GetAuctionItemsByType(EventIdent, "Silent Auction");
                siteInfo.Auctions.Add(siteInfo.SilentAuction);
                

                Sponsors pres = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Title");
                if (pres.Count > 0)
                {
                    siteInfo.PresentingSponsor = pres[0];
                }
                else
                { siteInfo.PresentingSponsor = null; }
                siteInfo.SponsorLevels.Add(pres);
                siteInfo.TitleSponsors = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Platinum");
                siteInfo.SponsorLevels.Add(siteInfo.TitleSponsors);
                siteInfo.EliteSponsors = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Elite");
                siteInfo.SponsorLevels.Add(siteInfo.EliteSponsors);
                siteInfo.GoldSponsors = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Gold");
                siteInfo.SponsorLevels.Add(siteInfo.GoldSponsors);
                siteInfo.ValetSponsor = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Valet");
                siteInfo.SponsorLevels.Add(siteInfo.ValetSponsor);
                siteInfo.SilverSponsors = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Silver");
                siteInfo.SponsorLevels.Add(siteInfo.SilverSponsors);
                siteInfo.RotarySponsors = MvcApplication.SiteDataMgr.GetSponsorsByLevel(EventIdent, "Rotary");
                siteInfo.SponsorLevels.Add(siteInfo.RotarySponsors);

                ctlr.Session["SiteInfo"] = siteInfo;
            }

            return siteInfo;
        }

        public CommonSiteInfo(DataManager dataMgr, Event EventIdent)
        {
            CommonSiteInfo.EventIdent = EventIdent;
            SilentAuction = dataMgr.GetAuctionItemsByType(EventIdent, "Silent Auction");
            LiveAuction = dataMgr.GetAuctionItemsByType(EventIdent, "Live Auction");

            Sponsors pres = dataMgr.GetSponsorsByLevel(EventIdent, "Presenting");
            if (pres != null && pres.Count > 0)
            {
                PresentingSponsor = pres[0];
            }
            TitleSponsors = dataMgr.GetSponsorsByLevel(EventIdent, "Title");
            EliteSponsors = dataMgr.GetSponsorsByLevel(EventIdent, "Elite");
            GoldSponsors = dataMgr.GetSponsorsByLevel(EventIdent, "Gold");
            ValetSponsor = dataMgr.GetSponsorsByLevel(EventIdent, "Valet");
            SilverSponsors = dataMgr.GetSponsorsByLevel(EventIdent, "Silver");
            RotarySponsors = dataMgr.GetSponsorsByLevel(EventIdent, "Rotary");

        }
    }
}