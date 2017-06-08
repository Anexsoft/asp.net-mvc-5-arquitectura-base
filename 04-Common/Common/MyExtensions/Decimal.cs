namespace Common.MyExtensions
{
    public static class Decimal
    {
        public static string ToCurrencyFormat(this decimal value)
        {
            return string.Format("{0:C2}", value);
        }

        public static string ToCurrencyFormat(this decimal? value)
        {
            if (value == null)
            {
                value = 0;
            }

            return string.Format("{0:C2}", value);
        }
    }
}
