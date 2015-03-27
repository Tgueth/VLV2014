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

        public TabSectionView(string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9, string str10, string str11, string str12)
        {
            pictureLink1 = str1;
            pictureLink2 = str2;
            header = str3;
            hLine1 = str4;
            hLine2 = str5;
            hLine3 = str6;
            lLine1 = str7;
            lLine2 = str8;
            lLine3 = str9;
            lLine4 = str10;
            lLine5 = str11;
            lLine6 = str12;
        }
    }
}