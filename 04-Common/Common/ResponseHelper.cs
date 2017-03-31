namespace Common
{
    public class ResponseHelper
    {
        public dynamic Result { get; set; }
        public bool Response { get; set; }
        public string Message { get; set; }
        public string Href { get; set; }
        public string Function { get; set; }

        public ResponseHelper()
        {
            Response = false;
            Message = "Ocurrio un error inesperado";
        }

        public void SetResponse(bool r, string m = "")
        {
            Response = r;
            Message = m;

            if (!r && m == "")
            {
                Message = "Ocurrio un error inesperado";
            }
        }
    }
}
