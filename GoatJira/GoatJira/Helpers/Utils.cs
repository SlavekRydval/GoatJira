namespace GoatJira.Helpers
{
    using System;

    class Utils
    {
        public static string ExceptionString(Exception E)
        {
            string result = E.Message;
            while (E.InnerException != null)
            {
                E = E.InnerException;
                result += "\n"+E.Message;
            }
            return result;
        }
    }
}
