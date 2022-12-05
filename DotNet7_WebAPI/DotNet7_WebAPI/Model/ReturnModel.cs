namespace DotNet7_WebAPI.Model
{
    public class RtCommon
    {
        public RtCommon()
        {
            isError= false;
        }
        public bool isError { get; set; }
        public MySqlConnector.MySqlErrorCode? mySqlErrorCode { get; set; }
        public string? excecptionString { get; set; }
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
