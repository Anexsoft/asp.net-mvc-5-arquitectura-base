namespace Common
{
    public abstract class ResponseHelperBase
    {
        public bool response { get; set; }
        public string message { get; set; }
        public string function { get; set; }
        public string href { get; set; }

        protected void PrepareResponse(bool r, string m = "")
        {
            response = r;

            if (r)
            {
                message = m;
            }
            else
            {
                message = (m == "" ? "An unexpected error occurred" : m);
            }
        }

        public ResponseHelperBase()
        {
            response = false;
            message = "An unexpected error occurred";
        }
    }

    public class ResponseHelper : ResponseHelperBase
    {
        public dynamic result { get; set; }

        public ResponseHelper SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }

    public class ResponseHelper<T> : ResponseHelperBase where T : class
    {
        public T result { get; set; }

        public ResponseHelper<T> SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }
}
