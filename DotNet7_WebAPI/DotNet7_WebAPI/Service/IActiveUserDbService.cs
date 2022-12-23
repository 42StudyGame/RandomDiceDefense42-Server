using DotNet7_WebAPI.Model;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public interface IActiveUserDbService
    {
        public RtActiveUserDb SetActiveUserInfo(string ID, string userInfo);
        public RtActiveUserDb GetActiveUserInfo(string ID);
    }
}
