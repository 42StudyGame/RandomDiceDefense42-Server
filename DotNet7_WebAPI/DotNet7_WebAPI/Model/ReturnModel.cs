namespace DotNet7_WebAPI.Model
{
    public class RtCommon
    {
        public bool isError { get; set; }
        public string excecptionString { get; set; }
    }

    public class RtAcountDb : RtCommon
    {
        public AccountDBModel data { get; set; }
    }
    public class RtLoginResult
    { 
        public string ID { get; set; }
        public string Token { get; set; }
    }

}
