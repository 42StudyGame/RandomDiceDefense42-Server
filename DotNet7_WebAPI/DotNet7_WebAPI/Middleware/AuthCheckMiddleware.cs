using DotNet7_WebAPI.Model;
using DotNet7_WebAPI.Service;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace DotNet7_WebAPI.Middleware
{
    public class ParsedInfos
    {
        public ParsedInfos()
        {
            RequestToken = null;
            ID= null;
            RequestStage = -1;
        }
        // header
        public string? RequestToken { get; set; }
        // body
        public string? ID { get; set; }
        public Int32? RequestStage { get; set; }
    }

    public class AuthCheckMiddleware
    {
        private readonly IActiveUserDbService _activeUserDb;
        private readonly IAccountDbService _accountDb;
        private readonly RequestDelegate _next;
        private ParsedInfos _parsedInfos;

        public AuthCheckMiddleware(RequestDelegate next, IActiveUserDbService redisDb, IAccountDbService myslqDb)
        {
            _next = next;
            _activeUserDb = redisDb;
            _accountDb= myslqDb;
            _parsedInfos = new ParsedInfos();
        }

        public async Task Invoke(HttpContext context)
        {
            var formString = context.Request.Path.Value;
            if (string.Compare(formString, "/login", StringComparison.OrdinalIgnoreCase) == 0 ||
                string.Compare(formString, "/register", StringComparison.OrdinalIgnoreCase) == 0)
            {
                await _next(context);

                return;
            }
            context.Request.EnableBuffering(); // 아래의 context.Request.Body.Position= 0;하기 위해서 필요.
            using (var reader = new System.IO.StreamReader(context.Request.Body, System.Text.Encoding.UTF8, true, 4096, true))
            {
                string? inputBody = await reader.ReadToEndAsync();
                if (await IsbodyFormValid(context, inputBody) == false)
                {
                    return;
                }
                if (await isTokenValid(context) == false)
                {
                    return;
                }
                context.Request.Body.Position= 0;
            }
            await _next(context);
            return;
        }
        private async Task<bool> IsbodyFormValid(HttpContext context, string? body)
        {
            if (string.IsNullOrEmpty(body))
            {
                // 그냥 잘못된 입력.
                await SetErrorInfo(context, 400, ErrorCode.WrongRequest);
                return false;
            }
            var document = JsonDocument.Parse(body);
            try
            {
                if (document.RootElement.TryGetProperty("ID", out var traceID) == false
                    || context.Request.Headers.TryGetValue("Token", out var traceToken) == false)
                {
                    // ID, token없는 잘못된 입력.
                    await SetErrorInfo(context, 400, ErrorCode.WrongRequest);
                    return false;
                }
                /////////////////////////////////////////////
                /// 바디나 헤더에서 추가적으로 파싱할 내용이 있다면 여기에 입력.
                document.RootElement.TryGetProperty("RequestStage", out var traceRequestStage);
                _parsedInfos.RequestStage = traceRequestStage.GetUInt16();
                _parsedInfos.ID = traceID.GetString();
                _parsedInfos.RequestToken = traceToken;
                /////////////////////////////////////////////
                return true;
            }
            catch(FormatException ex)
            {
                await SetErrorInfo(context, 400, ErrorCode.WrongRequest);
                return false;
            }
            catch(Exception ex)
            {
                // 기타 ㅇ예외들
                await SetErrorInfo(context, 500, ErrorCode.NotDefindedError);
                return false;
            }
        }

        private async Task<bool> isTokenValid(HttpContext context)
        {
            //ActiveUserModel? userInfoInDb;
            RtActiveUserDb userInfo = _activeUserDb.GetActiveUserInfo(_parsedInfos.ID);
            if (userInfo.errorCode != ErrorCode.NoError)
            {
                // 레디스에 유저 정보 없음
                await SetErrorInfo(context, 400, ErrorCode.WrongID);
                // body조작도 필요
                return false;
            }
            if(userInfo.Token.Equals(_parsedInfos.RequestToken) == false)
            {
                // 레디스에 저장된 토큰과 틀림. ->  클라에서 다시 로그인 하게 만들기.
                await SetErrorInfo(context, 400, ErrorCode.WrongToken);
                return false;
            }
            // 토큰 유효기간 연장?
            return true;
        }

        private async Task<bool> SetErrorInfo(HttpContext context, int statusCode, ErrorCode occuredErrorCode)
        {
            var errorJsonResponse = JsonSerializer.Serialize(new RsMiddleWare
            {
                errorCode = occuredErrorCode
            }) ;
            byte[] errorBytes = Encoding.UTF8.GetBytes(errorJsonResponse);
            context.Response.StatusCode = statusCode;
            await context.Response.Body.WriteAsync(errorBytes, 0, errorBytes.Length);
            return true;
        }
    }
}
