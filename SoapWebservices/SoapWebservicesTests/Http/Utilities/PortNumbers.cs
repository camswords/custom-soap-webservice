using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoapWebservicesTests.Http
{
    public class PortNumbers
    {
        public int LocateUnused()
        {
            return new Random().Next(40000, 63000);
        }
    }
}
