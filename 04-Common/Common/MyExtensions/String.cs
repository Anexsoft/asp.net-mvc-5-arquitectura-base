namespace Common.MyExtensions
{
    public static class String
    {
        #region Int Extensions
        public static string LeadingZeros(this int value, int n)
        {
            return value.ToString().PadLeft(n, '0');
        }
        #endregion
    }
}
