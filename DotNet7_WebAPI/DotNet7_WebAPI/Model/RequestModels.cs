namespace DotNet7_WebAPI.Model
{
    public class AuthModel
    {
        public string ID { set; get; }
        public string Token { set; get; }
    }
    public class RegisterInputModel
    {
        public string ID { set; get; }
        public string Password { set; get; }
    }
    public class LoginInputModel
    {
        public string ID { set; get; }
        public string Password { set; get; }
    }

    public class ActiveUserModel : IDisposable
    {
        public string ID { set; get; }
        //public string UserRank { set; get; }
        public string Token { set; get; }

        public void Dispose()
        {
            ;
        }
    }
    public class ScenarioRequestModel
    {
        public string ID { set; get; }
        public UInt16 RqusetState { set; get; }
    }
}
