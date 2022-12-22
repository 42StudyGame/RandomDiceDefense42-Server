using DotNet7_WebAPI.Model;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DotNet7_WebAPI.Service
{
    public interface IActiveUserDbService
    {
        public void SetActiveUserInfo(string ID, string userInfo);
        public string? GetActiveUserInfo(string ID);
    }
}
