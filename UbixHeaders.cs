namespace GlobalLib.Helpers
{
    public enum UbixHeaders
    {
        XCompany = 1
    }

    public static class UbixHeadersHelp
    {
        public static string GetString(UbixHeaders ubxh)
        {
            string retVal = "";

            switch (ubxh)
            {
                case UbixHeaders.XCompany:
                    retVal = "X-UBIX-COMPANY";
                    break;
                default:
                    break;
            }

            return retVal;
        }
    }
}