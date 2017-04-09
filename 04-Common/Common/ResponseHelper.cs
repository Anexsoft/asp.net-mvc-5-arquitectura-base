namespace Common
{
    public abstract class ResponseHelperBase
    {
        public bool Response { get; set; }
        public string Message { get; set; }
        public string Function { get; set; }
        public string Href { get; set; }

        protected void PrepareResponse(bool r, string m = "")
        {
            Response = r;

            if (r)
            {
                Message = m;
            }
            else
            {
                Message = (m == "" ? "An unexpected error occurred" : m);
            }
        }

        public ResponseHelperBase()
        {
            Response = false;
            Message = "An unexpected error occurred";
        }
    }

    public class ResponseHelper : ResponseHelperBase
    {
        public dynamic Result { get; set; }

        public ResponseHelper SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }

    public class ResponseHelper<T> : ResponseHelperBase where T : class
    {
        public T Result { get; set; }

        public ResponseHelper<T> SetResponse(bool r, string m = "")
        {
            PrepareResponse(r, m);
            return this;
        }
    }
}
