using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoapWebservices
{
    public class SuppressExceptions
    {
        public void On(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                // intentionally suppress exception
            }
        }
    }
}
