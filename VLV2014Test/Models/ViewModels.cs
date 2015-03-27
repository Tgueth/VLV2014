using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BinaryStarTechnology.CharityEventRevenueMgmtClasses;

namespace VLV2014Test.Models
{
    public class SponsorsView
    {
        public Sponsors Presenting { get; set; }
        public Sponsors Platinum { get; set; }
        public Sponsors Elite { get; set; }
        public Sponsors Gold { get; set; }
        public Sponsors Silver { get; set; }
        public Sponsors Rotarians { get; set; }
        public Sponsor Valet { get; set; }
        public Sponsor Music { get; set; } 
    }

    public class TabSectionView
    {
        public string pictureLink1 { get; set; }
        public string pictureLink2 { get; set; }
        public string header { get; set; }
        public string hLine1 { get; set; }
        public string hLine2 { get; set;}
        public string hLine3 { get; set; }
        public string lLine1 { get; set; }
        public string lLine2 { get; set; }
        public string lLine3 { get; set; }
        public string lLine4 { get; set; }
        public string lLine5 { get; set; }
        public string lLine6 { get; set; }
    }
}