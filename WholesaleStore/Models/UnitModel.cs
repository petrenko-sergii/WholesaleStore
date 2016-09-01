using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesaleStore.Models
{
    public class UnitModel
    {
        public List<Unit> Units { get; set; }
        public Unit UnitObject { get; set; }

        public UnitModel ()
	    {
            Units = new List<Unit>();
            UnitObject = new Unit();
	    }
    }
}