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
}