namespace DotNet7_WebAPI.Model
{
    public class AccountDBModel : IDisposable
    {
        public string ID { set; get; }
        public string HashedPassword { set; get; }
        public string Salt { set; get; }
        // 0 : 듀토리얼, 1 ~ 15 : 각 단계
        public UInt16 HighestStage { set; get; }
        public UInt64 HighestScore { set; get; } 
        public string UserRank { set; get; }

        public void Dispose()
        {
            ;
        }
    }
    public class DbConfig
    {
        public string AccountDB { set; get; }
        public string ActiveUserDb { set; get; }
    }
}
