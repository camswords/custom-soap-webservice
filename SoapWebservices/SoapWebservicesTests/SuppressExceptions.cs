using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoapWebservicesTests
{
    public class SuppressExceptions
    {
        public void On(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                // intentionally suppress exception
            }
        }
    }
}
