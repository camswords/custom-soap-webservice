using System;

namespace SoapWebservicesTest.Http.Utilities
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
