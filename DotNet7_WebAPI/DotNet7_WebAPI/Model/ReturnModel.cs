namespace DotNet7_WebAPI.Model
{
    public class RtCommon
    {
        public ErrorCode errorCode { get; set; }
    }

    public class RtAcountDb : RtCommon
    {
        public AccountDBModel data { get; set; }
    }
    public class RtActiveUserDb : RtCommon
    { 
        //public string ID { get; set; }
        public string Token { get; set; }
    }

    public class RtScenarioService : RtCommon
    {
        public string? Scenario { get; set; }
    }
}
