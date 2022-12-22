using DotNet7_WebAPI.Model;

namespace DotNet7_WebAPI.Service
{
    public interface IAccountDbService
    {
        public RtAcountDb RegisterAccount(AccountDBModel User);
        public RtAcountDb GetAccoutInfo(string id);
    }
}
