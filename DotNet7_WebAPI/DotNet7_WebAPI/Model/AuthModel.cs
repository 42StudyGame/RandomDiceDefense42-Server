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
    public class RegisterDBModel : IDisposable
    { 
        public string ID { set; get; }
        public string HashedPassword { set; get; }
        public string Salt { set; get; }

        public void Dispose()
        {
            ;
        }
    }

}
