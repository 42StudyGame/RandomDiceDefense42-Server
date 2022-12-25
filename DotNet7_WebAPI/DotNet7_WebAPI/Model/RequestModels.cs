namespace DotNet7_WebAPI.Model
{
    public class RqRegister
    {
        public string ID { set; get; }
        public string Password { set; get; }
    }
    public class RqLogin
    {
        public string ID { set; get; }
        public string Password { set; get; }
    }

    public class RqScenario
    {
        public string ID { set; get; }
        //public string Token { set; get; } <- 헤더로 받기.
        public int RequestStage { set; get; }
    }
}
